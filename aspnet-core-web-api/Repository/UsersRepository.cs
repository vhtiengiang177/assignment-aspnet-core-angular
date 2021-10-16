using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Repository
{
    public class UsersRepository
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
