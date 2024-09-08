using ArsaRExCH.Controllers;
using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace ArsaRExCH.InterfaceIMPL
{
    public class BetInterfaceIMPL : BetInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetController> _logger;

        public BetInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetController> logger,
                            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<Bet> CalculateBetResault(DateTime date, string id)
        {
            // First of first we calculate each bet resault  on 00:00  every day 
            //we get the bet and check the Guesses pair price in this case 
            // BTC if the price matches the current price (+ - 200 $) or any other amount the placed bet wi be x 7
            // in any other condition its lost position . 
            //User can not place bet for the same day . We set the available dates for bet .(Phase place bet )
            //We check in this case Binance publlic API to get price AT 00;00 
            //This sample method will return Final balance or final amount of user (intern)
            //about the current rewarding system maybe new thing will be come but not now , to make it more flixable to win
            var targetBets = await _context.Bet
                   .Where(c => c.UserIdSec == id && c.HitDateBTC.Date == date.Date) // Ensure both dates match to the day
                   .ToListAsync();

            // Fetch the user's wallet amount
            var userWallet = await _context.Wallet
                .FirstOrDefaultAsync(c => c.UserIDSec == id);

            // Replace 0 with the actual call to get the BTC price from Binance API
            var btcPrice = 0; // This should be replaced with actual price fetching logic

            // Iterate through each bet
            foreach (var bet in targetBets)
            {
                // Check if the bet price matches the current BTC price (+/- 200 USD)
                if (Math.Abs(bet.BtcPriceExpireBet - btcPrice) <= 200)
                {
                    // Bet is a winning one; update the bet and user wallet
                    bet.WiningAmount = bet.BetAmountBtc * 7;
                    bet.CompletedTime = DateTime.UtcNow;
                    bet.IsBetActive = false;

                    // Update user's wallet with the winning amount
                    if (userWallet != null)
                    {
                        userWallet.Amount += bet.BetAmountBtc * 7;
                    }

                    // Save changes to the database only for winning bets
                    await _context.SaveChangesAsync();
                }
            }

            // No specific Bet object to return as only updates are done; change return type to void if not used
            return null;
        }


            public async Task<string> Generatesha256()
        {
            return await Task.Run(() =>
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Generate a random byte array
                    byte[] randomBytes = new byte[32]; // 256 bits / 8 = 32 bytes
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(randomBytes);
                    }

                    // Compute the SHA-256 hash of the random bytes
                    byte[] hashBytes = sha256.ComputeHash(randomBytes);

                    // Convert the hash to a hexadecimal string
                    StringBuilder hashString = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hashString.Append(b.ToString("x2"));
                    }

                    return hashString.ToString();
                }
            });
        }

        public async Task<Bet> GetBetBySha(string sha)
        {
            // Assuming Bet has a property named Hash of type string
            return await _context.Bet
                .Where(c => c.BetSignutare == sha)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Bet>> GetBetsByUseId(string useId)
        {
            if (string.IsNullOrEmpty(useId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(useId));
            }

            try
            {
                var bets = await _context.Bet
                    .Where(c => c.UserIdSec == useId)
                    .ToListAsync();

                return bets;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the database update exception details here
                // For example: _logger.LogError(dbEx, "Database update error.");
                throw new ApplicationException("An error occurred while retrieving bets from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Log general exception details here
                // For example: _logger.LogError(ex, "An unexpected error occurred.");
                throw new ApplicationException("An unexpected error occurred while retrieving bets.", ex);
            }
        }


        public async Task SaveBet(Bet bet)
        {
            try
            {
                await _context.Bet.AddAsync(bet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "An error occurred while updating the database.");
                throw new Exception("There was a problem saving the bet to the database.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw new Exception("An unexpected error occurred while saving the bet.", ex);
            }
        }
    }
}
