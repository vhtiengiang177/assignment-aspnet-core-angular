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

namespace aspnet_core_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        UnitOfWork.UnitOfWork _unitOfWork;
        public ProductsController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(dataDbContext);
            _unitOfWork.Save();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            // return Ok(await _productsRepository.GetAllProducts().ToListAsync());
            return Ok(await _unitOfWork.ProductsRepository.GetAllProducts().ToListAsync());
        }

        [HttpGet("[action]/{productID}")]
        public IActionResult GetAProduct(int productID)
        {
            var _product = _unitOfWork.ProductsRepository.GetAProduct(productID);
            if(_product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_product);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateProduct([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductsRepository.CreateProduct(product);
                _unitOfWork.ProductsRepository.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("[action]/{productID}")]
        public IActionResult UpdateProduct(int productID, [FromForm] Product productObj)
        {
            if (ModelState.IsValid)
            {
                var isUpdate = _unitOfWork.ProductsRepository.UpdateProduct(productID, productObj);
                if (isUpdate)
                {
                    _unitOfWork.ProductsRepository.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("[action]/{productID}")]
        public IActionResult DeleteProduct(int productID)
        {
            bool isDelete = _unitOfWork.ProductsRepository.DeleteProduct(productID);
            if (isDelete)
            {
                _unitOfWork.ProductsRepository.SaveChanges();
                return Ok();
            }    
            return NotFound();
        }

        [HttpGet("[action]/{filter}")]
        public IActionResult SearchProductByNameOrCategory(int filter, [FromForm]string content)
        {
            List<Product> lProduct = _unitOfWork.ProductsRepository.SearchProductByNameOrCategory(filter, content);
            if(lProduct == null)
            {
                return NotFound();
            }
            return Ok(lProduct); 
        }

        [HttpGet("[action]")]
        public IActionResult QueryProductByPriceRange([FromForm] double min, [FromForm] double max)
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

    }
}
