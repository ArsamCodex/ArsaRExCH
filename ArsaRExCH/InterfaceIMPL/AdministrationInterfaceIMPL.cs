using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NBitcoin.Secp256k1;
using System.Security.Claims;

namespace ArsaRExCH.InterfaceIMPL
{
    public class AdministrationInterfaceIMPL : AdministrationInterface
    {



        private readonly PriceInterface _priceInterface;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContext;
        private readonly UserManager<ApplicationUser> UserManager;
        public AdministrationInterfaceIMPL(
                         PriceInterface priceInterface, IDbContextFactory<ApplicationDbContext> dbContext, UserManager<ApplicationUser> _UserManager)
        {


            _priceInterface = priceInterface;
            _dbContext = dbContext;

            UserManager = _UserManager;
        }

        public async Task AddAdminWarningMessage(AdminWarningMessage adminWarningMessage)
        {

            if (adminWarningMessage == null)
            {
                throw new ArgumentNullException(nameof(adminWarningMessage), "BanedCountries cannot be null.");
            }
            try
            {
                // Create a new context from the factory
                using var _context = _dbContext.CreateDbContext();
                await _context.AddAsync(adminWarningMessage);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can adjust this to your logging mechanism)
                Console.WriteLine($"Error adding banned country: {ex.Message}");
                throw new Exception("An unexpected error occurred while saving the Admin  Message.", ex);
            }

        }

        public async Task AddBannCountries(BanedCountries banedCountries)
        {
            // Check if banedCountries is null and throw ArgumentNullException
            if (banedCountries == null)
            {
                throw new ArgumentNullException(nameof(banedCountries), "BanedCountries cannot be null.");
            }

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

        public async Task<bool> CheckAdminSetupStatusAsync()
        {
            // Get the single AdminSetupInit record from the database
            using var context = _dbContext.CreateDbContext();
            var setupInit = await context.adminSetupInits.FirstOrDefaultAsync();

            // Return the ShowAdminSetupPopUp value, or false if not found
            return setupInit?.ShowAdminSetupPopUp ?? false;
        }


        public async Task DeleteAdminWarning(string id)
        {
            using var _context = _dbContext.CreateDbContext();
            // Find the AdminWarning entity by its ID
            var warning = await _context.adminWarningMessages.FindAsync(id);

            // If the warning is not found, throw an exception or handle it accordingly
            if (warning == null)
            {
                throw new KeyNotFoundException($"Warning with ID '{id}' was not found.");
            }

            // Remove the warning from the database context
            warning.IsDeleted = true;
            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<AdminSetupInit> EditAdminInit()
        {
            // Retrieve the existing AdminSetupInit record (assuming you know its ID)
            using var _context = _dbContext.CreateDbContext();

            var setupInit = await _context.adminSetupInits.FirstOrDefaultAsync();

            if (setupInit != null)
            {
                // Modify the properties you need to update
                setupInit.ShowAdminSetupPopUp = false; // Set this to the desired value

                // Save changes to the database
                await _context.SaveChangesAsync();

                return setupInit; // Return the updated record
            }

            // Optionally handle the case where the record is not found
            throw new InvalidOperationException("Admin setup initialization record not found.");
        }

        public async Task EditBTCWalletExchangeAmountDecrease(string userID, double amount)
        {

            var context = _dbContext.CreateDbContext();
            // Retrieve the wallet entry for the specified user and currency pair
            var wallet = await context.Wallet
                .Where(c => c.UserIDSec == userID && c.PairName == "BTC")
                .FirstOrDefaultAsync(); // Execute the query and get the first result

            // Check if the wallet entry exists
            if (wallet == null || amount < 0)
            {
                throw new InvalidOperationException("Wallet not found for the specified user.");
            }
            // Update the wallet amount
            //USE THIS METHOD TO EDIT WALLET WHEN BET PLACED
            wallet.Amount -= amount;
            if (wallet.Amount < 0)
            {
                throw new InvalidOperationException("Invalid Bet: Wallet balance cannot be negative.");
            }

            // Save the updated wallet entry to the database
            context.Wallet.Update(wallet);
            await context.SaveChangesAsync();

        }


        public async Task EditBTCWalletExchangeAmountIncrease(string userID, double amount)
        {
            // Create a new DbContext instance and ensure it is disposed of correctly
            using var context = _dbContext.CreateDbContext();

            // Fetch the wallet for the specified user and BTC pair
            var wallet = await context.Wallet
                .Where(c => c.UserIDSec == userID && c.PairName == "BTC")
                .FirstOrDefaultAsync();

            // Check if the wallet exists and if the amount to increase is valid
            if (wallet == null || amount < 0)
            {
                throw new InvalidOperationException("Wallet not found for the specified user, or amount is invalid.");
            }

            // Update the wallet amount by increasing it
            wallet.Amount += amount;

            // Save the changes to the database
            context.Wallet.Update(wallet);
            await context.SaveChangesAsync();
        }


        public async Task<AdminWarningMessage> GetAdminWarningMessage(DateTime date)
        {
            using var _context = _dbContext.CreateDbContext();

            // Fetch the message for the specified date
            return await _context.adminWarningMessages
                .Where(c => c.time.Date == date) // Assuming 'time' is a DateTime field
                .FirstOrDefaultAsync();
        }


        public async Task<List<AirDropFaq>> GetAllAirDropFaq()
        {
            try
            {
                using var context = _dbContext.CreateDbContext();
                return await context.airDropFaqs.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Gant gets airdopFaq", ex);
            }
        }

        public async Task<List<BanedCountries>> GetAllBannedCountriesInDatabase()
        {
            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext), "Value cannot be null.");
            try
            {
                using var context = _dbContext.CreateDbContext();
                return await context.BanedCountris.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get List of Banned countries", ex);
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

        public async Task<string?> GetScheduledMessageAsync()
        {
            using var context = _dbContext.CreateDbContext();
            var message = await context.adminWarningMessages
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(m => m.time) // Adjust this as needed
                .FirstOrDefaultAsync();

            return message?.Message;
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

        public async Task<List<string>> GetUserRolesAsync(ClaimsPrincipal user)
        {
            // Get the user ID from claims
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return new List<string>(); // User is not authenticated
            }

            // Create a new DbContext instance
            using var context = _dbContext.CreateDbContext();

            // Find the user using UserManager with the new context
            var appUser = await context.Users.FindAsync(userId);
            if (appUser == null)
            {
                return new List<string>(); // User not found
            }

            // Get roles for the user using the UserManager associated with the new context
            var roles = await UserManager.GetRolesAsync(appUser);
            return roles.ToList();
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

