using Microsoft.AspNetCore.Mvc;
using AngularApp.BackendAPI.Data;
using AngularApp.BackendAPI.Models;
using AngularApp.BackendAPI.DTOs;

namespace AngularApp.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = MockDataContext.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var role = MockDataContext.Roles.FirstOrDefault(x => x.Id == user.RoleId);

            var response = new AuthResponse
            {
                Token = "MockJWTToken_" + Guid.NewGuid().ToString(),
                FullName = user.FullName,
                Email = user.Email,
                RoleName = role?.Name ?? "User"
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (MockDataContext.Users.Any(x => x.Email == request.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            var newUser = new User
            {
                Id = MockDataContext.Users.Max(x => x.Id) + 1,
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId,
                DesignationId = request.DesignationId,
                GenderId = request.GenderId
            };

            MockDataContext.Users.Add(newUser);

            return Ok(new { message = "User registered successfully" });
        }
    }
}
