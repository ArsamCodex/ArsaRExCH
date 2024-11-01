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
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor to inject IHttpClientFactory
        public PrriceInterfaceIMPL(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<double> GetBtcPriceFromBinance()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("BinanceClient");

                var btcResponse = await httpClient.GetStringAsync("/api/v3/ticker/price?symbol=BTCUSDT");
                    var btcPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(btcResponse);

                    // Convert the price string to double
                    if (decimal.TryParse(btcPriceData.Price, out decimal btcPrice))
                    {
                        return (double)btcPrice; // Return as double
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching BTC price: {ex.Message}");
            }

            return 0;
        }

        public async Task<double> GetEthPriceFromBinance()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("BinanceClient");

                var ethResponse = await httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=ETHUSDT");
                    var ethPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(ethResponse);

                    // Convert the price string to double
                    if (decimal.TryParse(ethPriceData.Price, out decimal ethPrice))
                    {
                        return (double)ethPrice; // Return as double
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ETH price: {ex.Message}");
            }

            return 0;
        }
    }


    // Adjust the BinancePriceResponse class to match the API's response
    public class CryptoPriceResponse
    {
        [JsonPropertyName("price")]
        public string Price { get; set; }
    }

}
