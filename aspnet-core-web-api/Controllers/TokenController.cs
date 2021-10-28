using Domain.Entity;
using Infrastructure.Persistent;
using Infrastructure.Persistent.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration;
        private UnitOfWork _unitOfWork;

        public TokenController(IConfiguration configuration, DataDbContext dataDbContext)
        {
            this._configuration = configuration;
            _unitOfWork = new UnitOfWork(dataDbContext);
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] User userObj)
        {
            var user = await _unitOfWork.UsersRepository.GetAUser(userObj.Username, userObj.Password);
            if (user != null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", user.ID.ToString()),
                    new Claim("username", user.Username),
                    new Claim("isAdmin", user.IsAdmin.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], 
                    _configuration["Jwt:Audience"], 
                    claims, 
                    expires: DateTime.UtcNow.AddDays(10), 
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
