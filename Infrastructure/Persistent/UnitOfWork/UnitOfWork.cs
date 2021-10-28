using Domain.Entity;
using Domain.Infrastructure.Persistent;
using Infrastructure.Persistent.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.UnitOfWork
{
    public class UnitOfWork
    {
        private DataDbContext _dbContext;
        private IRepository<Category> _categoriesRepository;
        private IProductsRepository _productsRepository;
        private IProducts_CategoriesRepository _products_CategoriesRepository;
        private IRepository<Supplier> _suppliersRepository;
        private IUsersRepository _usersRepository;

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Category> CategoriesRepository
        {
            get
            {
                if (_categoriesRepository == null)
                    _categoriesRepository = new GenericRepository<Category>(_dbContext);
                return _categoriesRepository;
            }
        }

        public IProductsRepository ProductsRepository
        {
            get
            {
                if (_productsRepository == null)
                    _productsRepository = new ProductsRepository(_dbContext);
                return _productsRepository;
            }
        }

        public IProducts_CategoriesRepository Products_CategoriesRepository
        {
            get
            {
                if (_products_CategoriesRepository == null)
                    _products_CategoriesRepository = new Products_CategoriesRepository(_dbContext);
                return _products_CategoriesRepository;
            }
        }

        public IRepository<Supplier> SuppliersRepository
        {
            get
            {
                if (_suppliersRepository == null)
                    _suppliersRepository = new GenericRepository<Supplier>(_dbContext);
                return _suppliersRepository;
            }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_dbContext);
                return _usersRepository;
            }
        }

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

     }
}
