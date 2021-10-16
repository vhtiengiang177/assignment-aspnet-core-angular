using aspnet_core_web_api.Data;
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
        UnitOfWork.UnitOfWork _unitOfWork;
        public SuppliersController(DataDbContext dataDbContext)
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(dataDbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var lSuppliers = await _unitOfWork.SuppliersRepository.GetAllSuppliers();

            return Ok(lSuppliers);
        }
    }
}
