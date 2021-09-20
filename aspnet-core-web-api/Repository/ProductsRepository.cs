using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private DataDbContext _dbContext;

        public ProductsRepository(DataDbContext dataDbContext)
        {
            _dbContext = dataDbContext;
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _dbContext.Products;
        }

        public Product GetAProduct(int productID)
        {
            var product = _dbContext.Products.Find(productID);
            return product;
        }

        public void CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public bool UpdateProduct(int productID, Product productObj)
        {
            var product = _dbContext.Products.Find(productID);
            if (product == null)
            {
                return false;
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
                
                return true;
            }
        }

        public bool DeleteProduct(int productID)
        {
            var product = _dbContext.Products.Find(productID);
            if(product != null)
            {
                var productDetail = _dbContext.ProductDetails.Find(productID);
                var product_category = _dbContext.Product_Categories.Where(p => p.ProductID == productID).ToList();
                if (productDetail != null)
                    _dbContext.ProductDetails.Remove(productDetail);
                if (product_category != null)
                {
                    foreach (var item in product_category)
                    {
                        _dbContext.Product_Categories.Remove(item);
                    }
                }
                _dbContext.Products.Remove(product);
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public List<Product> SearchProductByNameOrCategory(int filter, string content)
        {
            List<Product> lProduct = new List<Product>();
            switch (filter)
            {
                case 0: // name
                    lProduct = _dbContext.Products.Where(p => p.Name.StartsWith(content)).ToList();
                    break;
                case 1: // category
                    var lProductItem = _dbContext.Product_Categories.Where(pc => pc.Category.Name.StartsWith(content)).Select(s => s.Product).ToList();
                    lProduct = lProductItem.GroupBy(gb => gb.ID).Select(s => s.First()).ToList();
                    break;
                default:
                    return null;
            }
            return lProduct;
        }

        public List<Product> QueryProductByPriceRange(double min, double max)
        {
            List<Product> lProduct = new List<Product>();
            lProduct = _dbContext.Products.Where(p => p.Price >= min && p.Price <= max).Select(p => p).ToList();
            return lProduct;
        }
    }
}
