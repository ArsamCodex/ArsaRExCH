using ArsaRExCH.Controllers;
using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class BetInterfaceIMPL : BetInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetController> _logger;

        public BetInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetController> logger,
                            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public async Task SaveBet(Bet bet)
        {
            try
            {
                await _context.Bet.AddAsync(bet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "An error occurred while updating the database.");
                throw new Exception("There was a problem saving the bet to the database.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw new Exception("An unexpected error occurred while saving the bet.", ex);
            }
        }
    }
}
