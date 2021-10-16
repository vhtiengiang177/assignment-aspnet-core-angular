using aspnet_core_web_api.Data;
using aspnet_core_web_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.UnitOfWork
{
    public class UnitOfWork
    {
        private DataDbContext _dbContext;
        private CategoriesRepository _categoriesRepository;
        private ProductsRepository _productsRepository;
        private Products_CategoriesRepository _products_CategoriesRepository;
        private SuppliersRepository _suppliersRepository;
        private UsersRepository _usersRepository;

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CategoriesRepository CategoriesRepository
        {
            get
            {
                if (_categoriesRepository == null)
                    _categoriesRepository = new CategoriesRepository(_dbContext);
                return _categoriesRepository;
            }
        }

        public ProductsRepository ProductsRepository
        {
            get
            {
                if (_productsRepository == null)
                    _productsRepository = new ProductsRepository(_dbContext);
                return _productsRepository;
            }
        }

        public Products_CategoriesRepository Products_CategoriesRepository
        {
            get
            {
                if (_products_CategoriesRepository == null)
                    _products_CategoriesRepository = new Products_CategoriesRepository(_dbContext);
                return _products_CategoriesRepository;
            }
        }

        public SuppliersRepository SuppliersRepository
        {
            get
            {
                if (_suppliersRepository == null)
                    _suppliersRepository = new SuppliersRepository(_dbContext);
                return _suppliersRepository;
            }
        }

        public UsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_dbContext);
                return _usersRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = _dbContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
