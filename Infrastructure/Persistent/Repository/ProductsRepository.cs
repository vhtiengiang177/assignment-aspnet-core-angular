using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Domain.Values;

namespace Infrastructure.Persistent.Repository
{
    public class ProductsRepository : IProductsRepository, IDisposable
    {
        private DataDbContext _dbContext;
        private bool disposed = false;

        public ProductsRepository(DataDbContext dataDbContext)
        {
            _dbContext = dataDbContext;
        }

        public async Task<IQueryable<Product>> GetAllProducts()
        {
            var lProduct = await _dbContext.Products.ToListAsync();
            return lProduct.AsQueryable();
        }

        public Product GetProductByID(int productID)
        {
            return _dbContext.Products.FirstOrDefault(p => p.ID == productID);
        }

        public Product CreateProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            return result.Entity;
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public IQueryable<Product> SortListProducts(string sort, IQueryable<Product> lProduct)
        {
            switch (sort)
            {
                case "rating:asc":
                    lProduct = lProduct.OrderBy(p => p.Rating).AsQueryable();
                    break;
                case "rating:desc":
                    lProduct = lProduct.OrderByDescending(p => p.Rating).AsQueryable();
                    break;
                case "price:asc":
                    lProduct = lProduct.OrderBy(p => p.Price).AsQueryable();
                    break;
                case "price:desc":
                    lProduct = lProduct.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                case "name:desc":
                    lProduct = lProduct.OrderByDescending(p => p.Name).AsQueryable();
                    break;
                case "name:asc":
                    lProduct = lProduct.OrderBy(p => p.Name).AsQueryable();
                    break;
                case "id:asc":
                    lProduct = lProduct.OrderBy(p => p.ID).AsQueryable();
                    break;
                default:
                    lProduct = lProduct.OrderByDescending(p => p.ID).AsQueryable();
                    break;
            }
            return lProduct;
        }

        public IQueryable<Product> FilterProduct(FilterParamsProduct searchParams, IQueryable<Product> lProductItems)
        {
            if (searchParams.MinPrice.HasValue)
                lProductItems = lProductItems.Where(x => x.Price >= searchParams.MinPrice);

            if (searchParams.MaxPrice.HasValue)
                lProductItems = lProductItems.Where(x => x.Price <= searchParams.MaxPrice);

            if (searchParams.Rating != null && searchParams.Rating.Count() != 0 && searchParams.Rating.Count() != 5)
                lProductItems = lProductItems.Where(x => searchParams.Rating.Contains(x.Rating));

            if (searchParams.Content != null)
                lProductItems = lProductItems.Where(p => p.Name.Contains(searchParams.Content)
                || p.Price.ToString().Contains(searchParams.Content));

            return lProductItems.AsQueryable();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
