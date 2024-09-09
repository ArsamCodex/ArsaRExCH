using ArsaRExCH.Components.Pages;
using ArsaRExCH.Interface;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArsaRExCH.InterfaceIMPL
{
    public class PrriceInterfaceIMPL : PriceInterface
    {
        private readonly HttpClient httpClient;
        private PrriceInterfaceIMPL(HttpClient httpClienti)
        {
            this.httpClient = httpClienti;
        }

        public async Task<double> GetBtcPriceFromBinance()
        {

                try
                {
                    // Fetch BTC price
                    var btcResponse = await httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT");
                    var btcPriceData = JsonSerializer.Deserialize<double>(btcResponse);
                    return btcPriceData;

 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching prices: {ex.Message}");
                }
            return 0;
        }
    }
}
