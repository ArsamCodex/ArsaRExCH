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
        public UserInterfaceIMPL(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<MaxInzetResultDTO>> CalculateMaxInzetAsync()
        {
            var results = new List<MaxInzetResultDTO>();

            try
            {
                // Query all bets from the database
                var allBets = await _context.Bet
                    .Where(b => !b.ISDeleted && b.IsBetActive)
                    .ToListAsync();

                // Group bets by HitDateBTC and HitDateETH
                var groupedBets = allBets
                    .GroupBy(b => b.HitDateBTC.Date)
                    .Union(allBets.GroupBy(b => b.HitDateETH.Date))
                    .Select(g => new
                    {
                        Date = g.Key,
                        BtcBets = g.Where(b => b.HitDateBTC.Date == g.Key).ToList(),
                        EthBets = g.Where(b => b.HitDateETH.Date == g.Key).ToList()
                    })
                    .ToList();

                foreach (var group in groupedBets)
                {
                    var result = new MaxInzetResultDTO
                    {
                        MyDateTime = group.Date,
                        HitBtcPrice = group.BtcBets.Any() ? group.BtcBets.First().BtcPriceExpireBet : 0,
                        MaxBtcTotalInBTC = group.BtcBets.Sum(b => b.BetAmountBtc),
                        HitEthPrice = group.EthBets.Any() ? group.EthBets.First().EthPriceExpireBet : 0, // Ensure EthPriceExpireBet exists in your Bet class
                        MaxEthTotal = group.EthBets.Sum(b => b.BetAmountETH) // Make sure this property exists
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
