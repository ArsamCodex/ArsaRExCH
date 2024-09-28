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
