using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArsaRExCH.DTOs;
using ArsarExchAPI.Service;

namespace ArsarExchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Data.ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;

        public LoginController(IConfiguration configuration,
                               SignInManager<Data.ApplicationUser> signInManager,JwtService jwtService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new LoginResult { Successful = false, Error = "Invalid login data." });

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email!, loginDTO.Password!, false, false);

            if (result.IsLockedOut)
                return BadRequest(new LoginResult { Successful = false, Error = "Account is locked. Please try again later." });

            if (result.RequiresTwoFactor)
                return BadRequest(new LoginResult { Successful = false, Error = "Two-factor authentication is required." });

            if (!result.Succeeded)
                return BadRequest(new LoginResult { Successful = false, Error = "Invalid credentials." });

            var user = await _signInManager.UserManager.FindByEmailAsync(loginDTO.Email!);

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.NameIdentifier, user.Id!),
        new Claim(ClaimTypes.Role, (await _signInManager.UserManager.GetRolesAsync(user)).FirstOrDefault() ?? "User")
            };

            var token = _jwtService.GenerateToken(claims);

            return Ok(new LoginResult { Successful = true, Token = token });
        }

    }

}

