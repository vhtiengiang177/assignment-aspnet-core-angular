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
    public class SuppliersController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public SuppliersController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork(dataDbContext);
        }

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var lSuppliers = _unitOfWork.SuppliersRepository.Get();

            return Ok(lSuppliers.ToList());
        }
    }
}
