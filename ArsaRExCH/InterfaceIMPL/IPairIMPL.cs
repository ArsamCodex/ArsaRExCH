using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using ArsaRExCH.StaticsHelper.CustomException;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArsaRExCH.InterfaceIMPL
{
    public class IPairIMPL(IDbContextFactory<ApplicationDbContext> _dbFactory , ILogger<IPairIMPL> logger ) : IPair
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory = _dbFactory;
        private readonly ILogger<IPairIMPL> _logger = logger;

        public async Task<bool> AddPair(Pair pair)
        {
            try
            {
                // Validate the input
                if (pair == null)
                {
                    throw new AddPairException("The pair object cannot be null.");
                }

                using var context = dbContextFactory.CreateDbContext();

                // Check if the pair already exists
                bool pairExists = await context.Pair.AnyAsync(p => p.PaiName == pair.PaiName);
                if (pairExists)
                {
                    throw new AddPairException($"Pair with the name '{pair.PaiName}' already exists.");
                }

                // Add the pair to the database
                await context.Pair.AddAsync(pair);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a pair.");
                throw new AddPairException("An unexpected error occurred while adding a pair.", ex);
            }
        }

        public async Task<List<Pair>> GetAllPairs()
        {
            try
            {
                //only reading data and don't need to update it, AsNoTracking() is a performance optimization.
                using var context = dbContextFactory.CreateDbContext();
                return await context.Pair.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library like Serilog, NLog, etc.)
                Console.Error.WriteLine($"Error retrieving pairs: {ex.Message} - {ex.StackTrace}");

                // Optionally wrap in a custom exception or rethrow
                throw new InvalidOperationException("An error occurred while fetching the pair list.", ex);
            }
        }

    }
}
