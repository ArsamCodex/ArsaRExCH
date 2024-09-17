using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Globalization;


namespace ArsaRExCH.StaticsHelper
{
    public class BackgroundServiceForBetResault : BackgroundService
    {
        private readonly IServiceScopeFactory _ServiceScope;

        public BackgroundServiceForBetResault(IServiceScopeFactory serviceScope)
        {
            _ServiceScope = serviceScope;
        }
        public async Task RunMyMethod()
        {

            using (var scope = _ServiceScope.CreateScope())
            {
                var date = DateTime.Now;
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var targetBets = await _context.Bet
                  .Where(c => c.HitDateBTC.Date == date.Date && c.IsBetActive == true) // Ensure both dates match to the day
                .ToListAsync();
                double btcPrice = 0;
                double ethPrice = 0;
                HttpClient httpClient = new HttpClient();

                try
                {
                    // Fetch BTC price

                    var btcResponse = await httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT");
                    var btcPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(btcResponse);
                    btcPrice = double.Parse(btcPriceData.Price);

                    Console.WriteLine(btcPrice);

                    var ethResponse = await httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=ETHUSDT");
                    var ethPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(ethResponse);
                    ethPrice = double.Parse(ethPriceData.Price);
                    Console.WriteLine(ethPrice);

                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"JSON error fetching prices: {jsonEx.Message}");
                }
                catch (FormatException formatEx)
                {
                    Console.WriteLine($"Format error converting prices: {formatEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching prices: {ex.Message}");
                }

                Console.WriteLine("wallet db call success");


                Console.WriteLine("wallet db call succses");
                // Iterate through each bet
                foreach (var bet in targetBets)
                {
                    Console.WriteLine("debug II");
                    // Check if the bet price matches the current BTC price (+/- 200 USD)
                    if (Math.Abs(bet.BtcPriceExpireBet - btcPrice) <= 200 && Math.Abs(bet.EthPriceExpireBet - ethPrice) <= 200)
                    {
                        // Bet is a winning one; update the bet and user wallet
                        // bet.WiningAmount = bet.BetAmountBtc * 7;
                        bet.CompletedTime = DateTime.Now;
                        bet.BtcPriceNow = btcPrice;
                        bet.EthPriceNow = ethPrice;
                        bet.IsBetActive = false;
                        bet.WiningAmount = bet.BetAmountBtc * 7;
                        bet.WiningAmountEth = bet.BetAmountETH * 7;


                        // Fetch the user's wallet amount
                        var userWallets = await _context.Wallet
                          .Where(c => c.UserIDSec == bet.UserIdSec)
                          .ToListAsync();


                        foreach (var wallet in userWallets)
                        {
                            if (wallet.PairName == "BTC")
                            {
                                wallet.Amount = bet.BetAmountBtc * 7;  // Replace with actual BTC amount
                            }
                            else if (wallet.PairName == "ETH")
                            {
                                wallet.Amount = bet.BetAmountETH * 7; // Replace with actual ETH amount
                            }

                        }



                        // Save changes to the database only for winning bets
                        await _context.SaveChangesAsync();
                        Console.WriteLine("db III");

                    }
                    else
                    {
                        //edit actual bet details like is active and pther details
                        bet.WiningAmount = 0;
                        bet.CompletedTime = DateTime.Now;
                        bet.BtcPriceNow = btcPrice;
                        bet.EthPriceNow = ethPrice;
                        bet.IsBetActive = false;
                        await _context.SaveChangesAsync();
                        Console.WriteLine("db IV , Operation completed");

                    }
                }
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate the delay until 00:01 for the current day
                TimeSpan delay = CalculateDelayUntilNextDay00();

                // Schedule the task to run at 00:01 every day
                await Task.Delay(delay, stoppingToken);

                // Perform your scheduled task here
                await RunMyMethod();

            }
        }
        /*First here wecalcuate every day 00:01 */
        private TimeSpan CalculateDelayUntilNextDay00()
        {
            DateTime now = DateTime.Now;
            DateTime next0000AM = now.Date.AddHours(00).AddMinutes(59);

            if (now > next0000AM)
            {
                next0000AM = next0000AM.AddDays(1);
            }
            return next0000AM - now;
        }

        private class CryptoPriceResponse
        {
            [JsonPropertyName("price")]
            public string Price { get; set; }
        }
    }
}