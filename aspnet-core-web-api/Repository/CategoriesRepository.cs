using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    public class CategoriesRepository
    {
        private DataDbContext _dbContext;
        public CategoriesRepository(DataDbContext dataDbContext)
        {
            _dbContext = dataDbContext;
        }

        public int GetLength()
        {
            return _dbContext.Categories.Count();
        }

        public async Task<IQueryable<Category>> GetAllCategories()
        {
            var lCategories = await _dbContext.Categories.ToListAsync();

            return lCategories.AsQueryable();
        }

    }
}
