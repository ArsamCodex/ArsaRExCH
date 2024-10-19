using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class ItradeIMPL : ITrade
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        public ItradeIMPL(IDbContextFactory<ApplicationDbContext> _dbContextFactory)
        {
            dbContextFactory = _dbContextFactory;
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
