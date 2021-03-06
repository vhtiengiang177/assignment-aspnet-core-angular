using Domain.Entity;
using Domain.Infrastructure.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Repository
{
    public class Products_CategoriesRepository : IProducts_CategoriesRepository
    {
        private DataDbContext _dbContext;

        public Products_CategoriesRepository(DataDbContext dataDbContext)
        {
            this._dbContext = dataDbContext;
        }

        public IQueryable<Product> GetProductsByCategoriesID(int[] idCategories)
        {
            int[] distinctIdCategories = idCategories.Distinct().ToArray();

            var lProductItem = _dbContext.Product_Categories
                                    .Where(pc => distinctIdCategories.Contains(pc.CategoryID))
                                    .Select(s => s.Product).ToList();

            return lProductItem.GroupBy(gb => gb.ID).Select(s => s.First()).AsQueryable();
        }

        public void AddCategoriesToProduct(Product_Category pcObj)
        {
            _dbContext.Product_Categories.Add(pcObj);
        }

        public void DeleteProducts_Categories(Product_Category pcObj)
        {
            _dbContext.Product_Categories.Remove(pcObj);
        }

        public IQueryable<Category> GetCategoriesByProductID(int idProduct)
        {
            var lCategoriesID = _dbContext.Product_Categories
                .Where(pc => pc.ProductID == idProduct).Select(pc => pc.Category);

            return lCategoriesID;
        }

        public IQueryable<Product_Category> GetPCByProductID(int idProduct)
        {
            var lPC = _dbContext.Product_Categories
                .Where(pc => pc.ProductID == idProduct);

            return lPC;
        }
    }
}
