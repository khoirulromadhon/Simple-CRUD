using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Simple_CRUD.Helpers;
using Simple_CRUD.Services;
using Simple_CRUD.ViewModels;

namespace Simple_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtHelper _jwt;

        public AuthController(AuthService authService, JwtHelper jwt)
        {
            _authService = authService;
            _jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(VMUsers request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { error = "Username and password are required." });

            var user = _authService.Register(request.Username, request.Password);
            return Ok(new { Message = "User registered successfully." });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(VMUsers request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { error = "Username and password are required." });

            var user = _authService.Login(request.Username, request.Password);

            if (user == null)
                return Unauthorized(new { error = "Invalid username or password." });

            var token = _jwt.GenerateToken(user);

            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out (client should discard token)." });
        }
    }
}
