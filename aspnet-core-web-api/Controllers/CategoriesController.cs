using Infrastructure.Persistent;
using Infrastructure.Persistent.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        public CategoriesController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork(dataDbContext);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var lCategories = _unitOfWork.CategoriesRepository.Get();

            return Ok(lCategories);
        }
    }
}
