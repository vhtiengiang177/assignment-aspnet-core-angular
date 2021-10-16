using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    public class SuppliersRepository
    {
        private DataDbContext _dbContext;
        public SuppliersRepository(DataDbContext dataDbContext)
        {
            _dbContext = dataDbContext;
        }

        public async Task<IQueryable<Supplier>> GetAllSuppliers()
        {
            var lSuppliers = await _dbContext.Suppliers.ToListAsync();

            return lSuppliers.AsQueryable();
        }
    }
}
