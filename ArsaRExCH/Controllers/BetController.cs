using ArsaRExCH.Data;
using ArsaRExCH.DTOs;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    [Authorize]
    public class BetController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetController> _logger;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public BetController(IConfiguration configuration, ApplicationDbContext context, ILogger<BetController> logger,
                               SignInManager<ApplicationUser> signInManager, AuthenticationStateProvider authenticationStateProvider, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
        }
        [HttpPost("GetBet")]
      //  [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> GetTheBet(Bet bet)
        {
            /*If you dont like to use efcore  use static class StaticMethods to work whit ado .net methods */
            try
            {
                // Attempt to add the bet to the database
                await _context.Bet.AddAsync(bet);
                await _context.SaveChangesAsync();

                // Return success response
                return Ok("Data Granted");
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions (e.g., constraint violations)
                // Log the exception details
                _logger.LogError(dbEx, "A database error occurred while adding a bet.");

                // Return a bad request with specific error message
                return BadRequest("An error occurred while saving the bet to the database.");
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions
                // Log the exception details
                _logger.LogError(ex, "An error occurred while processing the bet.");

                // Return a generic error response
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBtcPrice()
        {
            //Call To Binance endpoitn get data 
            return null;
        }

        [HttpGet("userid")]
        public async Task<string> GetUserId()
        {

            var userd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userd == null)
            {
                return "UserNot Autheticated";
            }
            return userd;

        }

       
    }
}
