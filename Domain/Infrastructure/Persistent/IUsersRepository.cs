using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Persistent
{
    public interface IUsersRepository
    {
        Task<User> GetAUser(string username, string password);
    }
}
