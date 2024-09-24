using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class AdministrationInterfaceIMPL : AdministrationInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetInterfaceIMPL> _logger;
        private readonly PriceInterface _priceInterface;
        private readonly ApplicationDbContext _dbContext;
        public AdministrationInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetInterfaceIMPL> logger,
                         SignInManager<ApplicationUser> signInManager, PriceInterface priceInterface, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _priceInterface = priceInterface;
            _dbContext = dbContext;
        }
        public async Task AddBannCountries(BanedCountries banedCountries)
        {
            try
            {
                await _context.AddAsync(banedCountries);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can adjust this to your logging mechanism)
                Console.WriteLine($"Error adding banned country: {ex.Message}");
                throw new Exception("An unexpected error occurred while saving the bet.", ex);
            }
        }


        public Task<BanedCountries> EditBannedCountries()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BanedCountries>> GetAllBannedCountriesInDatabase()
        {
            try
            {
                return await _context.BanedCountris.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get List of Baanned countries", ex);
            }
        }

        public Task<BanedCountries> RemoveBannedCuntries()
        {
            throw new NotImplementedException();
        }
    }
}
