using ArsaRExCH.Interface;
using Microsoft.AspNetCore.Components.Authorization;
namespace ArsaRExCH.StaticsHelper
{
    public class BackgroundServiceForBetResault : BackgroundService
    {
        private readonly ArsaRExCH.Interface.BetInterface _BetInterface;
        private readonly AuthenticationStateProvider _AuthenticationStateProvider;
        private BackgroundServiceForBetResault(ArsaRExCH.Interface.BetInterface betInterface, AuthenticationStateProvider authenticationStateProvider)
        {
            _BetInterface = betInterface;
            _AuthenticationStateProvider = authenticationStateProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            /*
             we set here mechanism for automation  other method nneede 
             */
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate the delay until 00:01 for the current day
                TimeSpan delay = CalculateDelayUntilNextDay00();

                // Schedule the task to run at 00:01 every day
                await Task.Delay(delay, stoppingToken);

                // Perform your scheduled task here
                //Here we get the date only and pass user Id
                var x = DateTime.UtcNow;
                /*
                 * One more check aut user get ID*/
                var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var TargetUser = "";
                if (user.Identity?.IsAuthenticated ?? false)
                {
                    TargetUser = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
                }

                await CheckBetResault(x, TargetUser);
            }
        }

        /*First here wecalcuate every day 00:01 */
        private TimeSpan CalculateDelayUntilNextDay00()
        {
            DateTime now = DateTime.Now;
            DateTime next0000AM = now.Date.AddHours(00).AddMinutes(01);

            if (now > next0000AM)
            {
                next0000AM = next0000AM.AddDays(1);
            }

            return next0000AM - now;
        }

        /*We cmake method which call ur interface to run Task */
        private async Task CheckBetResault(DateTime time,string id)
        {
            //call interfce for calculation 
            await _BetInterface.CalculateBetResault(time,id);
           
        }
    }
}
