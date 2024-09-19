using ArsaRExCH.Interface;
using System.Net;

namespace ArsaRExCH.InterfaceIMPL
{
    public class UserInterfaceIMPL : UserIpInterface
    {
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
                return "Unknown"; // Return "Unknown" if there's an error
            }
        }
    }
}
