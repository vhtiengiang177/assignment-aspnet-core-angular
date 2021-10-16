using aspnet_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    interface IProductsRepository : IDisposable
    {
        Task<IQueryable<Product>> GetAllProducts();
        Product GetAProduct(int productID);
        Product CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        List<Product> SearchProductByNameOrCategory(string name, string category);
        List<Product> QueryProductByPriceRange(double min, double max);
    }
}
