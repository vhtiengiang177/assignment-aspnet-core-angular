using aspnet_core_web_api.Data;
using aspnet_core_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UnitOfWork.UnitOfWork _unitOfWork;
        public UsersController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(dataDbContext);
        }

        [HttpGet]
        public async Task<User> GetUser(string username, string password)
        {
            return await _unitOfWork.UsersRepository.GetAUser(username, password);
        }
    }
}
