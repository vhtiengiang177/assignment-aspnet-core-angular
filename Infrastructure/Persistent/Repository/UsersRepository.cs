using Domain.Entity;
using Domain.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private DataDbContext _dbContext;
        public UsersRepository(DataDbContext dataDbContext)
        {
            this._dbContext = dataDbContext;
        }

        public async Task<User> GetAUser(string username, string password)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
