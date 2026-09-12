using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authservice) : ControllerBase
    {

        [HttpPost("Register")]

        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authservice.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("Username is already exist");
            }
                

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await authservice.LoginAsync(request);
            if(token is null)
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(token);
        }

        
      
    }
}
