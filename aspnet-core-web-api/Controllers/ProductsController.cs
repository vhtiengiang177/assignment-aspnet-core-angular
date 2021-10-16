using aspnet_core_web_api.Models;
using aspnet_core_web_api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using aspnet_core_web_api.DTO;
using AutoMapper.QueryableExtensions;

namespace aspnet_core_web_api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        UnitOfWork.UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(DataDbContext dataDbContext, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(dataDbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int? pageNumber, int? pageSize, string sort = null)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;

            var lProduct = await _unitOfWork.ProductsRepository.GetAllProducts();

            lProduct = _unitOfWork.ProductsRepository.SortListProducts(sort, lProduct);

            var lProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(lProduct.ToList());

            var response = new
            {
                totalPage = lProductDTO.Count(),
                data = lProductDTO.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).ToList()
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetAProduct(int id)
        {
            var product = _unitOfWork.ProductsRepository.GetAProduct(id);
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
                _unitOfWork.Save();
                try
                {
                    Product_Category pcObj = new Product_Category();
                    pcObj.ProductID = result.ID;
                    for (int i = 0; i < idCategories.Count(); i++)
                    {
                        pcObj.CategoryID = idCategories[i];
                        _unitOfWork.Products_CategoriesRepository.AddCategoriesToProduct(pcObj);
                        _unitOfWork.Save();
                    }
                    return Ok(result);
                }
                catch
                {
                    _unitOfWork.RollBack();
                    // nếu thêm categories bị lỗi thì xóa sp - false
                    _unitOfWork.ProductsRepository.DeleteProduct(result);
                    _unitOfWork.Save();
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
                    _unitOfWork.Save();

                    Product_Category pcObj = new Product_Category();
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
                    _unitOfWork.RollBack();
                    _unitOfWork.Save();
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
                var product = _unitOfWork.ProductsRepository.GetAProduct(id);

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

        [HttpGet("[action]")]
        public IActionResult SearchProductByNameOrCategory(string name, string category)
        {
            var lProduct =  _unitOfWork.ProductsRepository.SearchProductByNameOrCategory(name,category);
            if (lProduct == null)
            {
                return NotFound();
            }
            return Ok(lProduct); 
        }

        [HttpGet("[action]")]
        public IActionResult QueryProductByPriceRange(double min, double max)
        {
            if (min > max || min < 0)
                return BadRequest();
            List<Product> lProduct = _unitOfWork.ProductsRepository.QueryProductByPriceRange(min, max);
            if (lProduct == null)
            {
                return NotFound();
            }
            return Ok(lProduct);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProduct(int? pageNumber, int? pageSize, [FromQuery] int[] idCategories, string search, string sort = null)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;
            IQueryable<Product> lProductItems;

            if (idCategories.Length == _unitOfWork.CategoriesRepository.GetLength() || idCategories.Count() == 0)
            {
                lProductItems = await _unitOfWork.ProductsRepository.GetAllProducts();
            }
            else 
            {
                lProductItems = _unitOfWork.Products_CategoriesRepository.GetProductsByCategoriesID(idCategories);
            }

            var lProduct = _unitOfWork.ProductsRepository.SearchProductOfProductItems(search, lProductItems);

            lProduct = _unitOfWork.ProductsRepository.SortListProducts(sort, lProduct);

            var lProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(lProduct.ToList());

            var response = new
            {
                totalPage = lProductDTO.Count(),
                data = lProductDTO.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).ToList()
            };

            return Ok(response);
        }
    }
}
