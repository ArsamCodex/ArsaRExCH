using Nethereum.JsonRpc.Client;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace ArsaRExCH.StaticsHelper
{
    public class PriceHub : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Timer _timer;
        private bool _disposed = false;

        public PriceHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            // Set the timer to call SendPriceUpdates every 5 seconds
            _timer = new Timer(async _ => await TimerCallback(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private async Task TimerCallback()
        {
            // Check if the hub is disposed
            if (_disposed)
                return;

            await SendPriceUpdates();
        }

        public async Task SendPriceUpdates()
        {
            try
            {
                await FetchCryptoPrices();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendPriceUpdates: {ex.Message}");
                throw new HubException("An error occurred while fetching prices.", ex);
            }
        }

        private async Task FetchCryptoPrices()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("BinanceClient");

                // Fetch BTC price
                var btcResponse = await httpClient.GetStringAsync("api/v3/ticker/price?symbol=BTCUSDT");
                Console.WriteLine($"BTC API Response: {btcResponse}"); // Log the response
                var btcPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(btcResponse);

                if (btcPriceData != null && decimal.TryParse(btcPriceData.Price, out decimal btcPrice) && btcPrice > 0)
                {
                    Console.WriteLine($"BTC Price: {btcPrice}");
                    // Notify clients with the price
                    await Clients.All.SendAsync("ReceivePrices", btcPrice, 0); // Placeholder for ETH
                }
                else
                {
                    Console.WriteLine($"Failed to parse BTC price. Data: {btcPriceData?.Price}");
                }

                // Fetch ETH price
                var ethResponse = await httpClient.GetStringAsync("api/v3/ticker/price?symbol=ETHUSDT");
                Console.WriteLine($"ETH API Response: {ethResponse}"); // Log the response
                var ethPriceData = JsonSerializer.Deserialize<CryptoPriceResponse>(ethResponse);

                if (ethPriceData != null && decimal.TryParse(ethPriceData.Price, out decimal ethPrice) && ethPrice > 0)
                {
                    Console.WriteLine($"ETH Price: {ethPrice}");
                    // Notify clients with the price
                    await Clients.All.SendAsync("ReceivePrices", 0, ethPrice); // Placeholder for BTC
                }
                else
                {
                    Console.WriteLine($"Failed to parse ETH price. Data: {ethPriceData?.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching prices: {ex.Message}");
                throw; // Rethrow the exception to be handled by the hub
            }
        }


        private class CryptoPriceResponse
        {
            [JsonPropertyName("price")] // Ensure the property matches the JSON response
            public string Price { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true; // Mark as disposed
                _timer?.Dispose(); // Dispose of the timer
            }
            base.Dispose(disposing);
        }
    }
}