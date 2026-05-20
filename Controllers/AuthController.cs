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
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

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
            // Security Issue 1: No input validation
            if (request == null)
                return BadRequest(new { message = "Invalid request" });

            if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
                return BadRequest(new { message = "Invalid email format" });

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
                return BadRequest(new { message = "Password must be at least 8 characters" });

            if (string.IsNullOrWhiteSpace(request.FullName) || request.FullName.Length > 100)
                return BadRequest(new { message = "Invalid full name" });

            // Security Issue 2: User enumeration (reveals email existence)
            if (MockDataContext.Users.Any(x => x.Email == request.Email))
            {
                return BadRequest(new { message = "Registration request could not be processed" });
            }

            var newUser = new User
            {
                Id = MockDataContext.Users.Max(x => x.Id) + 1,
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = 2,  // Default "User" role, hardcoded server-side
                DesignationId = request.DesignationId,
                GenderId = request.GenderId
            };

            // Security Issue 7: Logging sensitive data
            _logger.LogInformation("New user registered successfully");

            MockDataContext.Users.Add(newUser);

            // Security Issue 8: Returning sensitive data
            return Ok(new
            {
                message = "User registered successfully"
            });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
