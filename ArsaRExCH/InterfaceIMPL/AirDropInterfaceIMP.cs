using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArsaRExCH.InterfaceIMPL
{
    public class AirDropInterfaceIMP : AirDropInterface
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        public AirDropInterfaceIMP(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<int> AirDropWalletBalance(string id)
        {
            try
            {
                using var context = dbContextFactory.CreateDbContext();
                var currentBalance = await context.AirDrops
                    .Where(c => c.ApplicationUser.Id == id)
                    .Select(c => c.HowMannyClickInTottal) // Select just the balance
                    .FirstOrDefaultAsync();

                // Check if currentBalance is null and handle accordingly
                if (currentBalance == default) // Default is 0 for int
                {
                    Console.WriteLine("No AirDrop entry found for this user or balance is 0.");
                    return 0; // No entry found or balance is 0
                }

                Console.WriteLine($"Current balance: {currentBalance}");
                return currentBalance;
            }
            catch (Exception ex)
            {
                throw new Exception("Airdrop get data failed: " + ex.Message, ex); // Include original exception
            }
        }


        public async Task<AirDrop> GetAirDropById(string id)
        {
            using var context = dbContextFactory.CreateDbContext();
            var airDrop = await context.AirDrops
                .Where(c => c.ApplicationUserId == id)
                .FirstOrDefaultAsync();

            if (airDrop != null)
            {
                return airDrop;
            }

            // No AirDrop found, return null instead of throwing an exception
            return null;
        }

        public async Task<bool> IncrementAndSaveAirDrop(string userId)
        {
            using var context = dbContextFactory.CreateDbContext();

            // Retrieve the AirDrop associated with the user
            var airDrop = await context.AirDrops.FirstOrDefaultAsync(a => a.ApplicationUserId == userId);

            if (airDrop != null)
            {
                // Increment the click count
                airDrop.HowMannyClickInTottal += 1;
                airDrop.TimeClick = DateTime.Now; // Update the click time

                // Save the changes
                context.AirDrops.Update(airDrop);
                await context.SaveChangesAsync();
                return true;
            }

            // If no existing AirDrop is found, return false or handle accordingly
            return false;
        }


        public async Task<bool> SaveDrop(AirDrop airDrop)
        {
            using var _context = dbContextFactory.CreateDbContext();

            // Ensure that airDropID is not set explicitly
            if (airDrop != null)
            {
                _context.AirDrops.Add(airDrop); // No need to set AirDropID here
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
