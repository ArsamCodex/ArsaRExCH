using ArsaRExCH.Data;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace ArsaRExCH.StaticsHelper
{
    public class BackgroundServiceForBetResault : BackgroundService
    {


        private readonly IServiceScopeFactory _ServiceScope;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public  BackgroundServiceForBetResault(IServiceScopeFactory serviceScope, IHttpContextAccessor httpContextAccessor )
        {
            _ServiceScope = serviceScope;
            _httpContextAccessor = httpContextAccessor;
           


        }
       

        public async Task RunMyMethod()
        {
            var MyUser = await GetUserId();
            using (var scope = _ServiceScope.CreateScope())
            {
                /*Check thisi method  GetUserId*/
                try
                {
                    
                    if (MyUser == null)
                    {
                        Console.WriteLine("UsrId null");
                    }
                    Console.WriteLine(MyUser);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                var date = DateTime.Now;


                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var targetBets = await _context.Bet
                  .Where(c => c.UserIdSec == MyUser && c.HitDateBTC.Date == date.Date &&c.IsBetActive==true) // Ensure both dates match to the day
                  .ToListAsync();

                Console.WriteLine(targetBets.Count);

                // Fetch the user's wallet amount
                var userWallet = await _context.Wallet
                    .FirstOrDefaultAsync(c => c.UserIDSec == MyUser);

                // var btcPrice = await _priceInterface.GetBtcPriceFromBinance(); // This should be replaced with actual price fetching logic
                var btcPrice = 50000;
                Console.WriteLine("wallet db call succses");
                // Iterate through each bet
                foreach (var bet in targetBets)
                {Console.WriteLine("debug II");
                    // Check if the bet price matches the current BTC price (+/- 200 USD)
                    if (Math.Abs(bet.BtcPriceExpireBet - btcPrice) <= 200)
                    {
                        // Bet is a winning one; update the bet and user wallet
                        bet.WiningAmount = bet.BetAmountBtc * 7;
                        bet.CompletedTime = DateTime.Now;
                        bet.BtcPriceNow = btcPrice;
                        bet.IsBetActive = false;
                        

                        // Update user's wallet with the winning amount
                        if (userWallet != null)
                        {
                            userWallet.Amount += bet.BetAmountBtc * 7;
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
                        bet.IsBetActive = false;
                        await _context.SaveChangesAsync();


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
                //Here we get the date only and pass user Id

                   await RunMyMethod();
              //  await GetUserId();

            }
        }


        /*First here wecalcuate every day 00:01 */
        private TimeSpan CalculateDelayUntilNextDay00()
        {
            DateTime now = DateTime.Now;
            DateTime next0000AM = now.Date.AddHours(21).AddMinutes(04);

            if (now > next0000AM)
            {
                next0000AM = next0000AM.AddDays(1);
            }

            return next0000AM - now;
        }

        /*We cmake method which call ur interface to run Task */

        public async Task<string> GetUserId()
        {
           
            var user = _httpContextAccessor.HttpContext?.User;
            /*
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }*/
            var userid = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userid);

            return userid;
        }

    }

}
