using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Infrastructure.Persistent
{
    public interface IProducts_CategoriesRepository
    {
        IQueryable<Product> GetProductsByCategoriesID(int[] idCategories);
        void AddCategoriesToProduct(Product_Category pcObj);
        void DeleteProducts_Categories(Product_Category pcObj);
        IQueryable<Category> GetCategoriesByProductID(int idProduct);
        IQueryable<Product_Category> GetPCByProductID(int idProduct);

    }
}
