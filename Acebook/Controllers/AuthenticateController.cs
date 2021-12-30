using System;
using System.Threading.Tasks;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Acebook.AuthenticateController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtFactory _jwtFactory;

        public AuthenticateController(UserManager<ApplicationUser> userManager, JwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { Status = "Error", Message = "User already exists" });
            }

            var user = new ApplicationUser()
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { Status = "Error", Message = "User creation failed" });
            }

            return Ok(new AuthResponseDto { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                TokenDto tokenDto = _jwtFactory.GenerateToken(user, Guid.NewGuid(), DateTime.UtcNow);
                return Ok(tokenDto);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> Status()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { Username = user.UserName });
        }
    }
}
