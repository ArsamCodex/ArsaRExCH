using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;
using NBitcoin.Secp256k1;

namespace ArsaRExCH.InterfaceIMPL
{
    public class ItradeIMPL : ITrade
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        public ItradeIMPL(IDbContextFactory<ApplicationDbContext> _dbContextFactory)
        {
            dbContextFactory = _dbContextFactory;
        }

        public async Task CheckAndFIlledOrder(double btcprice, double userprice)
        {
            var _context = dbContextFactory.CreateDbContext();
            double tolerance = 100.0; // Set tolerance as double

            // Fetch all trades from the database directly within this method
            var trades = await _context.Trade
                .Where(trade => !trade.IsTradeDone && !trade.IsMarketBuy) // Assuming IsTradeDone and MarketBuy are properties
                .ToListAsync();

            foreach (var trade in trades)
            {
                // Check if the order price is within the defined tolerance
                if (trade.symbolI >= (btcprice - tolerance) && trade.symbolI <= (btcprice + tolerance))
                {
                    // Optionally, you can check against the user price as well
                    if (trade.symbolI >= (userprice - tolerance) && trade.symbolI <= (userprice + tolerance))
                    {
                        trade.IsTradeDone = true; // Mark the trade as done
                                                  // Set other properties or perform additional logic as needed
                    }
                }
            }

            // Save changes back to the database
            await _context.SaveChangesAsync();
        }
    



    public async Task<List<Trade>> GetAllTrades()
        {
            try
            {
                await using var context = dbContextFactory.CreateDbContext();
                return await context.Trade
                     .Include(t => t.User) // Load User
                     .Include(t => t.BitcoinPool) // Load BitcoinPool
                     .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update related exceptions
                Console.WriteLine($"Database update error: {dbEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Handle invalid operations, such as an issue with the DbContext
                Console.WriteLine($"Invalid operation: {invalidOpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        public async Task<double> GetTradeFee()
        {
           await using var context = dbContextFactory.CreateDbContext();

            try
            {
                // Fetch the first available trade fee, or return a default value if none exists
                return await context.tradeFees
                    .Select(c => c.FeeInBtc)
                    .FirstOrDefaultAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions, log details if needed
                Console.Error.WriteLine($"Database error while fetching trade fee: {dbEx.Message}");
                throw new Exception("A database error occurred while retrieving the trade fee.", dbEx);
            }
            catch (InvalidOperationException invEx)
            {
                // Handle cases where there might be issues with the query itself
                Console.Error.WriteLine($"Invalid operation error: {invEx.Message}");
                throw new Exception("An invalid operation occurred while retrieving the trade fee.", invEx);
            }
            catch (Exception ex)
            {
                // Handle any other types of exceptions
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving the trade fee.", ex);
            }
        }


        public async Task SaveFeeTrade(TradeFee tradeFee)
        {
          
            try
            {
                using var _context = dbContextFactory.CreateDbContext();
                await _context.tradeFees.AddAsync(tradeFee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log detailed exception information
                Console.Error.WriteLine($"DbUpdateException: {ex.Message}");
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while saving the trade.", ex);
            }
            catch (Exception ex)
            {
                // Log any other exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task SaveTrade(Trade trade)
        {
            // Validate the trade object
            if (trade == null)
            {
                throw new ArgumentNullException(nameof(trade), "Trade cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(trade.TradeId))
            {
                throw new ArgumentException("Trade ID is required.", nameof(trade.TradeId));
            }

            if (trade.symbolI <= 0)
            {
                throw new ArgumentException("Symbol I must be greater than zero.", nameof(trade.symbolI));
            }

            if (trade.SymbolII <= 0)
            {
                throw new ArgumentException("Symbol II must be greater than zero.", nameof(trade.SymbolII));
            }

            try
            {
                using var _context = dbContextFactory.CreateDbContext();
                await _context.Trade.AddAsync(trade);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log detailed exception information
                Console.Error.WriteLine($"DbUpdateException: {ex.Message}");
                Console.Error.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while saving the trade.", ex);
            }
            catch (Exception ex)
            {
                // Log any other exceptions
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }


    }
}
