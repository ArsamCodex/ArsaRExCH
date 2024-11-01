using ArsaRExCH.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArsaRExCH.StaticsHelper
{/*
    public class DaailyMessageChedcker : BackgroundService
    {
        
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DaailyMessageChedcker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Check for today's messages
                    await CheckForAdminWarningMessage(stoppingToken);

                    // Calculate the delay until the next run
                    var delay = CalculateDelayUntilNextRun();
                    if (delay.TotalMilliseconds > 0)
                    {
                        Console.WriteLine($"Waiting for {delay.TotalMinutes} minutes until the next check.");
                        await Task.Delay(delay, stoppingToken);
                    }
                    else
                    {
                        Console.WriteLine("The calculated delay is less than or equal to zero, executing immediately.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred in ExecuteAsync: {ex.Message} - {ex.StackTrace}");
                }
            }
        }

        private async Task CheckForAdminWarningMessage(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            try
            {
                var adminWarningService = scope.ServiceProvider.GetRequiredService<AdministrationInterface>();

                var today = DateTime.Today;
                var message = await adminWarningService.GetAdminWarningMessage(today);

                // Process the message as needed
                if (message != null)
                {
                    Console.WriteLine($"Admin warning message for {today}: {message}");
                }
                else
                {
                    Console.WriteLine($"No admin warning message found for {today}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching admin warning message: {ex.Message} - {ex.StackTrace}");
            }
        }

        private TimeSpan CalculateDelayUntilNextRun()
        {
            // Target time for daily execution (19:13)
            var targetTime = DateTime.Today.AddHours(19).AddMinutes(13);
            Console.WriteLine($"Target run time is set for: {targetTime}");

            // If the target time has already passed for today, schedule it for tomorrow
            if (DateTime.Now > targetTime)
            {
                targetTime = targetTime.AddDays(1);
                Console.WriteLine("Target time has passed. Scheduling for tomorrow.");
            }

            var delay = targetTime - DateTime.Now;
            Console.WriteLine($"Next run scheduled in: {delay}");

            return delay;
        }
    }*/
}
