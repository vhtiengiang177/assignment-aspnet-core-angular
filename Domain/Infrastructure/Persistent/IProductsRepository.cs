using Domain.Entity;
using Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Repository
{
    public interface IProductsRepository : IDisposable
    {
        Task<IQueryable<Product>> GetAllProducts();
        Product GetProductByID(int productID);
        Product CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        IQueryable<Product> SortListProducts(string sort, IQueryable<Product> lProduct);
        IQueryable<Product> FilterProduct(FilterParamsProduct searchParams, IQueryable<Product> lProductItems);
    }
}
