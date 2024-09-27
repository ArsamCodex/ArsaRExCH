using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArsaRExCH.InterfaceIMPL
{
    public class AdministrationInterfaceIMPL : AdministrationInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetInterfaceIMPL> _logger;
        private readonly PriceInterface _priceInterface;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContext;
        public AdministrationInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetInterfaceIMPL> logger,
                         SignInManager<ApplicationUser> signInManager, PriceInterface priceInterface, IDbContextFactory<ApplicationDbContext> dbContext)
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
                // Create a new context from the factory
                using var context = _dbContext.CreateDbContext();

                await context.AddAsync(banedCountries);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can adjust this to your logging mechanism)
                Console.WriteLine($"Error adding banned country: {ex.Message}");
                throw new Exception("An unexpected error occurred while saving the country ban.", ex);
            }
        }

        public async Task<List<ApplicationUser>> AllUsers()
        {
            try
            {
                using var context = _dbContext.CreateDbContext();
                return await context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Some Problem");
            }
        }

        public async Task<List<BanedCountries>> GetAllBannedCountriesInDatabase()
        {
            try
            {
                using var context = _dbContext.CreateDbContext();
                return await context.BanedCountris.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get List of Baanned countries", ex);
            }
        }

        public async Task<List<UserDatesRecord>> GetAllUserDates(string userID)
        {
            using var context = _dbContext.CreateDbContext();
            var userDates = await context.UserDatesRecords
                  .Where(ud => ud.ApplicationUserId == userID) // Filter by userID
                  .ToListAsync();
            return userDates;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            try
            {
                using var context = _dbContext.CreateDbContext();

                // Fetch the user from the database by their ID, including UserLoginDates
                var user = await context.Users
                    .Include(u => u.UserLoginDates) // Include related UserLoginDates
                    .FirstOrDefaultAsync(u => u.Id == userId);

                // Check if the user was found
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID '{userId}' was not found.");
                }

                return user;
            }
            catch (KeyNotFoundException ex)
            {
                // Log the exception (optional) and rethrow for higher-level handling
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception (connection issues, etc.)
                Console.WriteLine("Database update error: " + ex.Message);
                throw new Exception("An error occurred while retrieving the user from the database.");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                throw new Exception("An error occurred while retrieving the user.");
            }
        }



        public async Task<bool> RemoveBannedCuntries(int banbId)
        {
            try
            {
                using var context = _dbContext.CreateDbContext();
                var bannedCountry = await context.BanedCountris.FindAsync(banbId);
                if (bannedCountry != null)
                {
                    context.BanedCountris.Remove(bannedCountry);
                    await context.SaveChangesAsync(); // Save changes to the database
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing country: {ex.Message}");
                return false;
            }
        }
    }
}

