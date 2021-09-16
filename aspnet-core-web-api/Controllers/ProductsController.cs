using aspnet_core_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private DataDbContext _dbContext;

        public ProductsController(DataDbContext dataDbContext)
        {
            _dbContext = dataDbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_dbContext.Products);
        }

        [HttpGet("[action]/{productID}")]
        public async Task<IActionResult> GetAProduct(int productID)
        {
            var _product = await _dbContext.Products.FindAsync(productID);
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
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("[action]/{productID}")]
        public async Task<IActionResult> UpdateProduct(int productID, [FromBody] Product productObj)
        {
            var product = await _dbContext.Products.FindAsync(productID);
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                product.Name = productObj.Name;
                product.Description = productObj.Description;
                product.ReleaseDate = productObj.ReleaseDate;
                product.DiscontinuedDate = productObj.DiscontinuedDate;
                product.Rating = productObj.Rating;
                product.ReleaseDate = productObj.ReleaseDate;
                product.SupplierID = product.SupplierID;

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpDelete("[action]/{productID}")]
        public async Task<IActionResult> DeleteProduct(int productID)
        {
            var product = await _dbContext.Products.FindAsync(productID);
            var productDetail = await _dbContext.ProductDetails.FindAsync(productID);
            var product_category = _dbContext.Product_Categories.Where(p => p.ProductID == productID).ToList();
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                if (product_category != null)
                {
                    foreach (var item in product_category)
                    {
                        _dbContext.Product_Categories.Remove(item);
                    }
                }
                if (productDetail != null)
                {
                    _dbContext.ProductDetails.Remove(productDetail);
                }
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpGet("[action]/{filter}")]
        public IActionResult QueryProductByNameOrCategory(int filter, [FromForm]string search = "B")
        {
            List<Product> lProduct = new List<Product>();
            switch (filter)
            {
                case 0: // name
                    lProduct = _dbContext.Products.Where(p => p.Name.Contains(search)).ToList();
                    break;
                case 1: // category
                    var lProductItem = _dbContext.Product_Categories.Where(pc => pc.Category.Name.Contains(search)).Select(s=>s.Product).ToList();
                    lProduct = lProductItem.GroupBy(gb => gb.ID).Select(s => s.First()).ToList();
                    break;
                default:
                    return NoContent();
            }
            if(lProduct == null)
            {
                return NoContent();
            }
            return Ok(lProduct); 
        }

        [HttpGet("[action]")]
        public IActionResult QueryProductByPriceRange([FromForm] double min, double max)
        {
            if (min > max)
                return NotFound();
            List<Product> lProduct = new List<Product>();
            lProduct = _dbContext.Products.Where(p => p.Price >= min && p.Price <= max).Select(p => p).ToList();
            if (lProduct == null)
            {
                return NoContent();
            }
            return Ok(lProduct);
        }
    }
}
