﻿using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aspnet_core_web_api.Repository
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

        public Product GetAProduct(int productID)
        {
            return _dbContext.Products.Find(productID);
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
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

        public List<Product> SearchProductByNameOrCategory(string name, string category)
        {
            List<Product> lProduct = new List<Product>();
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(category))
            {
                var lProductItem = _dbContext.Product_Categories
                                    .Where(pc => pc.Category.Name.Contains(category) && pc.Product.Name.Contains(name))
                                    .Select(s => s.Product).ToList();
                lProduct = lProductItem.GroupBy(gb => gb.ID).Select(s => s.First()).ToList();
            }
            else if (!string.IsNullOrEmpty(category))
            {
                var lProductItem = _dbContext.Product_Categories
                        .Where(pc => pc.Category.Name.Contains(category))
                        .Select(s => s.Product).ToList();
                lProduct = lProductItem.GroupBy(gb => gb.ID).Select(s => s.First()).ToList();
            }
            else
            {
                lProduct = _dbContext.Products.Where(p => p.Name.Contains(name)).ToList();
            }
            return lProduct;
        }

        public List<Product> QueryProductByPriceRange(double min, double max)
        {
            List<Product> lProduct = new List<Product>();
            lProduct = _dbContext.Products.Where(p => p.Price >= min && p.Price <= max).Select(p => p).ToList();
            return lProduct;
        }

        public IQueryable<Product> SearchProductOfProductItems(string search, IQueryable<Product> lProductItems)
        {
            return lProductItems.Where(p => p.Name.Contains(search)).AsQueryable();
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
