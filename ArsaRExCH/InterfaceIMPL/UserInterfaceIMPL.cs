using ArsaRExCH.Data;
using ArsaRExCH.DTOs;
using ArsaRExCH.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ArsaRExCH.InterfaceIMPL
{
    public class UserInterfaceIMPL : UserIpInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContext;
        public UserInterfaceIMPL(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public async Task<List<MaxInzetResultDTO>> CalculateMaxInzetAsync()
        {
            var results = new List<MaxInzetResultDTO>();

            try
            {
                var cont = _dbContext.CreateDbContext();
                // Query all bets from the database
                var allBets = await cont.Bet
                    .Where(b => !b.ISDeleted && b.IsBetActive)
                    .ToListAsync();

                // Collect all unique dates from HitDateBTC and HitDateETH
                var uniqueDates = allBets
                    .Select(b => b.HitDateBTC.Date)
                  //  .Union(allBets.Select(b => b.HitDateETH.Date))
                    .Distinct()  // Ensure dates are unique
                    .ToList();

                foreach (var date in uniqueDates)
                {
                    var btcBets = allBets.Where(b => b.HitDateBTC.Date == date).ToList();
                    var ethBets = allBets.Where(b => b.HitDateETH.Date == date).ToList();

                    var result = new MaxInzetResultDTO
                    {
                        MyDateTime = date,
                        HitBtcPrice = btcBets.Any() ? btcBets.First().BtcPriceExpireBet : 0,
                        MaxBtcTotalInBTC = btcBets.Sum(b => b.BetAmountBtc),
                        HitEthPrice = ethBets.Any() ? ethBets.First().EthPriceExpireBet : 0,
                        MaxEthTotal = ethBets.Sum(b => b.BetAmountETH)
                    };

                    results.Add(result);
                }

                Console.WriteLine($"Querying for all dates.");
                Console.WriteLine($"Found {results.Count} unique dates with bets.");
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error: {ex.Message}");
            }

            return results;
        }




        public async Task<string> CheckIfIpIsBanned(string userIpAddress)
        {
            try
            {
                // Retrieve the list of banned IP addresses (which are already two digits) from the database
                var bannedIpAddresses = await _context.BanedCountris
                    .Where(b => !string.IsNullOrEmpty(b.IpAdressToBann))
                    .Select(b => b.IpAdressToBann)
                    .ToListAsync();

                Console.WriteLine("Banned IP Addresses:");
                foreach (var ip in bannedIpAddresses)
                {
                    Console.WriteLine(ip);
                }

                string userIpFirstTwoDigits = ExtractFirstPart(userIpAddress);
               // Console.WriteLine($"User IP First Two Digits: {userIpFirstTwoDigits}");

                foreach (var bannedIp in bannedIpAddresses)
                {
                   // Console.WriteLine($"Comparing with banned IP: {bannedIp}");
                    if (userIpFirstTwoDigits == bannedIp)
                    {

                        Console.WriteLine("IP match found. User is banned.");
                        return "You are banned from accessing this service.";
                    }
                }

                Console.WriteLine("No match found. User is not banned.");
                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
                // Return empty if not banned
        }
        public string ExtractFirstPart(string ipAddress)
        {
            try
            {
                // Regex to capture all digits before the first dot
                var match = Regex.Match(ipAddress, @"^(\d+)");

                if (match.Success)
                {
                    // Return all digits before the first dot
                    return match.Groups[1].Value;
                }

                return "Invalid IP Format";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> GetPublicIpAddress()
        {
            try
            {
                // Fetch the external IP address as a string
                string externalIpString = new WebClient().DownloadString("http://icanhazip.com").Trim();
                // Parse the string into an IPAddress object
                var externalIp = IPAddress.Parse(externalIpString);
                return externalIp.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return ex.Message; // Return "Unknown" if there's an error
            }
        }
    }
}
