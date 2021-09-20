using aspnet_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    interface IProductsRepository
    {
        IQueryable<Product> GetAllProducts();
        Product GetAProduct(int productID);
        void CreateProduct(Product product);
        bool UpdateProduct(int productID, Product productObj);
        bool DeleteProduct(int productID);
        void SaveChanges();
        List<Product> SearchProductByNameOrCategory(int filter, string content);
        List<Product> QueryProductByPriceRange(double min, double max);
    }
}
