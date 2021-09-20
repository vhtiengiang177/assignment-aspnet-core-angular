using aspnet_core_web_api.Data;
using aspnet_core_web_api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.UnitOfWork
{
    public class UnitOfWork
    {
        private DataDbContext _dbContext;
        private ProductsRepository _productsRepository;
        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductsRepository ProductsRepository
        {
            get
            {
                if (_productsRepository == null)
                {
                    _productsRepository = new ProductsRepository(_dbContext);
                }
                return _productsRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
