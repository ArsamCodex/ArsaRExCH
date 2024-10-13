using ArsaRExCH.Interface;

namespace ArsaRExCH.StaticsHelper
{
    public class DaailyMessageChedcker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;



        public DaailyMessageChedcker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Check for today's messages
                    await CheckForAdminWarningMessage();

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
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private async Task CheckForAdminWarningMessage()
        {
            using var scope = _serviceProvider.CreateScope();
            var adminWarningService = scope.ServiceProvider.GetRequiredService<AdministrationInterface>();

            var today = DateTime.Today;
            var message = await adminWarningService.GetAdminWarningMessage(today);

        }

        private TimeSpan CalculateDelayUntilNextRun()
        {
            // Target time for daily execution (10:12 PM)
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
    }
}