using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using ArsaRExCH.StaticsHelper.CustomException;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException();
        }
    }
}
