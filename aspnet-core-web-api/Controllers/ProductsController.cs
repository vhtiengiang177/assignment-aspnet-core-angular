using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Persistent.UnitOfWork;
using Infrastructure.Persistent;
using Domain.Entity;
using Domain.Values;

namespace aspnet_core_web_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public ProductsController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork(dataDbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterParamsProduct filterParams)
        {
            int currentPageNumber = filterParams.PageNumber ?? 1;
            int currentPageSize = filterParams.PageSize ?? 5;

            IQueryable<Product> lProductItems;

            if(filterParams.IdCategories != null)
            {
                if (filterParams.IdCategories.Count() != 0 
                    || filterParams.IdCategories.Count() != _unitOfWork.CategoriesRepository.Count())
                {
                    lProductItems = _unitOfWork.Products_CategoriesRepository.GetProductsByCategoriesID(filterParams.IdCategories);
                }
                else lProductItems = await _unitOfWork.ProductsRepository.GetAllProducts();
            }
            else lProductItems = await _unitOfWork.ProductsRepository.GetAllProducts();

            lProductItems = _unitOfWork.ProductsRepository.FilterProduct(filterParams, lProductItems);

            var lProduct = _unitOfWork.ProductsRepository.SortListProducts(filterParams.Sort, lProductItems);

            var response = new ResponseJSON<Product>
            {
                TotalPage = lProduct.Count(),
                Data = lProduct.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).ToList()
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByID(int id)
        {
            var product = _unitOfWork.ProductsRepository.GetProductByID(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var categories = _unitOfWork.Products_CategoriesRepository.GetCategoriesByProductID(id);
                var response = new
                {
                    product = product,
                    categories = categories

                };
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product, [FromQuery] int[] idCategories)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.ProductsRepository.CreateProduct(product);

                if(_unitOfWork.Save())
                {
                    bool success = true;
                    Product_Category pcObj = new Product_Category();
                    pcObj.ProductID = result.ID;
                    int[] distinctIdCategories = idCategories.Distinct().ToArray();
                    for (int i = 0; i < distinctIdCategories.Count(); i++)
                    {
                        pcObj.CategoryID = distinctIdCategories[i];
                        _unitOfWork.Products_CategoriesRepository.AddCategoriesToProduct(pcObj);
                        if (!_unitOfWork.Save())
                        {
                            success = false;
                            break;
                        }
                    }

                    if (success)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        _unitOfWork.ProductsRepository.DeleteProduct(result);
                        _unitOfWork.Save();
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] Product productObj, [FromQuery] int[] idCategories)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProductsRepository.UpdateProduct(productObj);

                    var lPC = _unitOfWork.Products_CategoriesRepository.GetPCByProductID(productObj.ID);

                    foreach (var item in lPC)
                    {
                        _unitOfWork.Products_CategoriesRepository.DeleteProducts_Categories(item);
                    }

                    if (!_unitOfWork.Save())
                    {
                        return BadRequest();
                    }

                    Product_Category pcObj = new Product_Category();
                    idCategories = idCategories.Distinct().ToArray();
                    pcObj.ProductID = productObj.ID;

                    foreach (var item in idCategories)
                    {
                        pcObj.CategoryID = item;
                        _unitOfWork.Products_CategoriesRepository.AddCategoriesToProduct(pcObj);
                        _unitOfWork.Save();
                    }

                    return Ok(productObj);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _unitOfWork.ProductsRepository.GetProductByID(id);

                if (product == null)
                    return NotFound();

                _unitOfWork.ProductsRepository.DeleteProduct(product);
                _unitOfWork.Save();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
