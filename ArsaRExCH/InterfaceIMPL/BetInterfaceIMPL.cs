using ArsaRExCH.Controllers;
using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using ArsaRExCH.DTOs;

namespace ArsaRExCH.InterfaceIMPL
{
    public class BetInterfaceIMPL : BetInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetInterfaceIMPL> _logger;
        private readonly PriceInterface _priceInterface;

        public BetInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetInterfaceIMPL> logger,
                            SignInManager<ApplicationUser> signInManager, PriceInterface priceInterface)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _priceInterface = priceInterface;
        }

        public async Task CalculateBetResault(DateTime date, string id)
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
            var userWallets = await _context.Wallet
        .Where(c => c.UserIDSec == id)
        .ToListAsync();

            // Replace 0 with the actual call to get the BTC price from Binance API
            var btcPrice = await _priceInterface.GetBtcPriceFromBinance();
            var ethPrice = await _priceInterface.GetEthPriceFromBinance();

            // This should be replaced with actual price fetching logic
            // Iterate through each bet
            foreach (var bet in targetBets)
            {
                // Check if the bet price matches the current BTC price (+/- 200 USD)
                if (Math.Abs(bet.BtcPriceExpireBet - btcPrice) <= 200 && Math.Abs(bet.EthPriceExpireBet - ethPrice) <= 200)
                {
                    // Bet is a winning one; update the bet and user wallet
                  //  bet.WiningAmount = bet.BetAmountBtc * 7;
                    bet.CompletedTime = DateTime.UtcNow;
                    bet.BtcPriceNow = btcPrice;
                    bet.EthPriceNow = ethPrice;

                    bet.IsBetActive = false;

                    // Update user's wallet with the winning amount
                
                        foreach (var wallet in userWallets)
                        {
                            if (wallet.PairName == "BTC")
                            {
                                wallet.Amount = bet.BetAmountBtc * 7;  // Replace with actual BTC amount
                            }
                            else if (wallet.PairName == "ETH")
                            {
                                wallet.Amount = bet.BetAmountETH*7; // Replace with actual ETH amount
                            }

                        }
                    

                    // Save changes to the database only for winning bets
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //edit actual bet details like is active and pther details
                    bet.WiningAmount = 0;
                    bet.CompletedTime = DateTime.UtcNow;
                    bet.BtcPriceNow = btcPrice;
                    bet.EthPriceNow= ethPrice;
                    bet.IsBetActive = false;
                    await _context.SaveChangesAsync();
                    

                }
            }

       
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

        public async Task<UserAnalyticsDTO> UserTradeAnalytics(string userId)
        {
            // Fetch bets from the database using the context
            var bets = await _context.Bet
                                     .Where(bet => bet.UserIdSec == userId )
                                     .ToListAsync();

            // Calculate the total number of trades
            int tradeCount = bets.Count;

            // Calculate the number of wins
            int winCount = bets.Count(bet => bet.WiningAmount.HasValue && bet.WiningAmount > 0);

            // Calculate the number of losses
            int lossCount = tradeCount - winCount;

            // Calculate the total win amount
            double totalWin = bets.Where(bet => bet.WiningAmount.HasValue)
                                  .Sum(bet => bet.WiningAmount.Value);

            double totalWinETH = bets.Where(bet => bet.WiningAmountEth.HasValue)
                                 .Sum(bet => bet.WiningAmountEth.Value);

            // Calculate the total amount placed in bets
            double totalInzet = bets.Sum(bet => bet.BetAmountBtc + bet.BetAmountETH);

            var winningBetsETH = bets
              .Where(bet => bet.WiningAmountEth.HasValue && bet.WiningAmountEth > 0)
              .Sum(bet => bet.WiningAmountEth.Value);


            // Get the winning bets and include the additional details for BTC

            var winningBets = bets
                .Where(bet => bet.WiningAmount.HasValue && bet.WiningAmount > 0)
                .Select(bet => new Bet
                {
                    BetId = bet.BetId,
                    BtcPriceNow = bet.BtcPriceNow,
                    HitDateBTC = bet.HitDateBTC,
                    BtcPriceExpireBet = bet.BtcPriceExpireBet,
                    BetAmountBtc = bet.BetAmountBtc,
                    WiningAmount = bet.WiningAmount,
                    WiningAmountEth = winningBetsETH,
                    
                })
                .ToList();

            // Create the DTO object to return
            var userAnalyticsDTO = new UserAnalyticsDTO
            {
                TradeCount = tradeCount,
                WinCOunt = winCount,
                LossCount = lossCount,
                TottalWIn = totalWin,
                TottalWInEth = winningBetsETH,
                TottalInzet = totalInzet,
                PairName = "BTC/ETH", // You can customize this field as needed
                WiningBets = winningBets
            };

            return userAnalyticsDTO;
        }


    }
}
