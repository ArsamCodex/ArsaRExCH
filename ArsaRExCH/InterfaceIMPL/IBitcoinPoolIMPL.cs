using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class IBitcoinPoolIMPL : IBitcoinPool
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public IBitcoinPoolIMPL(IDbContextFactory<ApplicationDbContext> dbContextFactory) { 
            _dbContextFactory = dbContextFactory;
        }
        public async Task EditPool(BitcoinPool bitcoinPool)
        {
            using var _context = _dbContextFactory.CreateDbContext();

            // Retrieve the existing pool from the database
            var existingPool = await _context.BitcoinPools.FindAsync(bitcoinPool.BitcoinPoolId);

            if (existingPool != null)
            {
                // Update the pool properties
                existingPool.PoolName = bitcoinPool.PoolName;
                existingPool.PoolCurrentBalance = bitcoinPool.PoolCurrentBalance;
                existingPool.Daterefilled = bitcoinPool.Daterefilled;

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Bitcoin pool not found.");
            }
        }

        public async Task<List<BitcoinPoolTransactions>> GetBitcoinPoolTransactions()
        {
            try
            {
                using var _context = _dbContextFactory.CreateDbContext();

                // Retrieve all pools
                return await _context.poolTransactions.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (if necessary) and handle it accordingly
                Console.WriteLine($"An error occurred while retrieving pools: {ex.Message}");

                // Return an empty list or handle as per your requirement
                throw new Exception("Error Get Pools", ex);

            }

        }

        public async Task<List<BitcoinPool>> GetPools()
        {
            try
            {
                using var _context = _dbContextFactory.CreateDbContext();

                // Retrieve all pools
                return await _context.BitcoinPools.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (if necessary) and handle it accordingly
                Console.WriteLine($"An error occurred while retrieving pools: {ex.Message}");

                // Return an empty list or handle as per your requirement
                throw new Exception("Error Get Pools", ex);
                
            }
        }

        public async Task InitBitcoinPool(BitcoinPool bitcoinPool)
        {

            try
            {
                using var _context = _dbContextFactory.CreateDbContext();
                await _context.BitcoinPools.AddAsync(bitcoinPool);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Save BitcoinPool", ex);
            }
        }
    }
}
