using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new();

        [HttpPost("Register")]

        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

    }
}
