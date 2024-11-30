using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArsarExchAPI.Service;
using ArsarExchAPI.Model.DTOS;
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
                               SignInManager<Data.ApplicationUser> signInManager, JwtService jwtService)
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
            /*
             * 
             * 
                The ! character in loginDTO.Email! is the null-forgiving operator in C#. It tells the compiler to assume that the value is not null, 
                 even if the variable is declared as nullable.
             * */
            var user = await _signInManager.UserManager.FindByEmailAsync(loginDTO.Email!);
            /*
             * What are Claims?
                A claim is a key-value pair that represents information about a user (e.g., email, role, ID).
                Claims are typically used in token-based authentication to pass information about the user securely.
             * 
             * 2. Line-by-Line Breakdown
                    a) var claims = new[]
                    This initializes an array of Claim objects, each representing a piece of information about the user.
                    b) new Claim(ClaimTypes.Email, user.Email!)
                    Adds a claim for the user's email address.
                    ClaimTypes.Email: A standard claim type used to identify email addresses in claims-based authentication.
                    user.Email!: The ! (null-forgiving operator) asserts that user.Email is not null. If user.Email can be null, it would throw a NullReferenceException.
                    c) new Claim(ClaimTypes.NameIdentifier, user.Id!)
                    Adds a claim for the user's unique identifier (e.g., User ID).
                    ClaimTypes.NameIdentifier: A standard claim type used to identify the user uniquely.
                    user.Id!: Similarly, the ! operator asserts that user.Id is not null.
                    d) new Claim(ClaimTypes.Role, (await _signInManager.UserManager.GetRolesAsync(user)).FirstOrDefault() ?? "User")
                    Adds a claim for the user's role (e.g., "Admin", "User").
                    ClaimTypes.Role: A standard claim type used to represent the user's role in the system.
                    (await _signInManager.UserManager.GetRolesAsync(user)):
                    Retrieves a list of roles assigned to the user from the identity system asynchronously.
                    .FirstOrDefault() ?? "User":
                    Extracts the first role from the list. If no roles are found, defaults to "User". This ensures the claim always has a value.
             * 
             *  Why Are Claims Used?
                        Claims are included in the JWT token to store user-specific information securely.
                        They allow the API or any authenticated system to identify the user and their permissions without querying the database.
             * */
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

