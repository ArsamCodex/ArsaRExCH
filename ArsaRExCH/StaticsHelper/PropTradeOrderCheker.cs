
using ArsaRExCH.Data;
using ArsaRExCH.Model.Prop;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using NBitcoin.Secp256k1;

namespace ArsaRExCH.StaticsHelper
{
    public class PropTradeOrderCheker(IServiceScopeFactory ServiceScope) : BackgroundService
    {
        private readonly IServiceScopeFactory _ServiceScope = ServiceScope;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Execute your method here
                    await CheckOrders();

                    // Wait for 5 seconds
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    Console.WriteLine($"Error in {nameof(PropTradeOrderCheker)}: {ex.Message}");
                }
            }
        }


        private async Task CheckOrders()
        {
            using (var scope = _ServiceScope.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Check the database first for pairs
                var AvailablePairs = await context.prepPirs.Select(x => x.PairName).ToListAsync(); // Extract only pair names

                // Get the pair data from API call
                using HttpClient client = new HttpClient();
                string url = "https://fapi.binance.com/fapi/v1/ticker/price";

                // Fetch JSON response from Binance API
                var response = await client.GetStringAsync(url);
            // Console.WriteLine(response);

              
                var allPrices = JsonConvert.DeserializeObject<List<PrepPairDTO>>(response);
            
                
               var filteredPrices = allPrices
                  .Where(p => AvailablePairs.Contains(p.PairName)) // Compare with pair names
                  .Select(p => new { p.PairName, p.Price }) // Include both PairName and Price
                  .ToList();
                foreach (var price in filteredPrices)
                {
                    Console.WriteLine($"PairName: {price.PairName}, Price: {price.Price}");
                }
                var filteredPairs2 = filteredPrices.Select(p => p.PairName).ToList(); // Extract only PairNames from filteredPrices

                var DatasToCHeck = await context.propTrdaes
                             .Where(c => (c.Status == OrderStatus.Open || c.Status == OrderStatus.Pending)
                                         && filteredPairs2.Contains(c.PairName))// Check if the pair matches
                             .ToListAsync();
                Console.WriteLine($"\nFound {DatasToCHeck.Count} matching records in propTrdaes.");



                
                foreach (var x in DatasToCHeck)
                {
                    var pairPrice = filteredPrices.FirstOrDefault(p => p.PairName == x.PairName)?.Price;
                    Console.WriteLine(pairPrice);

                    var btcDecimal = decimal.TryParse(pairPrice, out var parsedPrice) ? parsedPrice : 0m;
                    var SecUserId = x.PropUserId;

                    var EditUserX = await context.propUsers
                        .Where(u => u.PropUserId == x.PropUserId)
                        .FirstOrDefaultAsync();
                    var UserId = EditUserX.ApplicationUserId;




                    if (x.orderTypeProp == OrderTypeProp.Buy && x.MarketType == MarketType.Market)
                    {
                        var pnl = Math.Round(CalculateProfitLossInDollar(
                              x.OrderPriceOpened,
                              btcDecimal,
                              x.AmountForOrder,
                              x.Leverage, x.orderTypeProp, x.MarketType, x.Status
              ),
                      1
                          );
                        // Stop-loss activated if the price has hit or dropped below StopLoss
                        if (btcDecimal <= x.StopLoss)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.StopLoss;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder - Math.Abs(pnl));

                            await context.SaveChangesAsync();
                        }
                        if (btcDecimal >= x.TakeProfit)
                        {


                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.TakeProfit;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder + Math.Abs(pnl));

                            await context.SaveChangesAsync();

                        }

                        var liquidationPriceMaxRange = x.AmountForOrder;
                        decimal liquidationPrice = x.OrderPriceOpened * (1 - 1 / x.Leverage);
                        //liquidation max range is for example 50
                        //if pnl is -50 trade liquidated
                        if (pnl <= -liquidationPriceMaxRange)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.Liquidated;
                            await context.SaveChangesAsync();
                        }

                    }
                    if (x.orderTypeProp == OrderTypeProp.Sell && x.MarketType == MarketType.Market)
                    {
                        var pnl = Math.Round(CalculateProfitLossInDollar(
                               x.OrderPriceOpened,
                               btcDecimal,
                               x.AmountForOrder,
                               x.Leverage, x.orderTypeProp, x.MarketType, x.Status),
                       1
                           );
                        // Stop-loss activated if the price has hit or dropped below StopLoss
                        if (btcDecimal >= x.StopLoss)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.StopLoss;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder - Math.Abs(pnl));

                            await context.SaveChangesAsync();

                        }
                        if (btcDecimal <= x.TakeProfit)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.TakeProfit;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder + Math.Abs(pnl));

                            await context.SaveChangesAsync();


                        }

                        var liquidationPriceMaxRange = x.AmountForOrder;
                        decimal liquidationPrice = x.OrderPriceOpened * (1 - 1 / x.Leverage);
                        //liquidation max range is for example 50
                        //if pnl is -50 trade liquidated
                        if (pnl <= -liquidationPriceMaxRange)
                        {
                            x.OrderPriceClosed = btcDecimal ;
                            x.Status = OrderStatus.Liquidated;
                            await context.SaveChangesAsync();
                        }

                    }
                    decimal targetPrice = x.OrderPriceOpened; // The target price for opening/closing the order
                    decimal priceRange = 1m; // $10 range

                    // Check if it's a Buy order, in the correct market type, and in Pending status
                    if (x.orderTypeProp == OrderTypeProp.Buy && x.MarketType == MarketType.Order && x.Status == OrderStatus.Pending)
                    {
                        // Open the order only if the price is within a $10 range of the target price
                        if (btcDecimal >= targetPrice - priceRange && btcDecimal <= targetPrice + priceRange)
                        {
                            x.Status = OrderStatus.Open;
                            x.OrderPriceTriggerd = btcDecimal * 1.001m;
                            x.OrderTriggerdAt = DateTime.Now;


                            await context.SaveChangesAsync();
                            Console.WriteLine($"Order opened at price: {btcDecimal}");
                        }
                        //RTODO second side if price under
                        if (btcDecimal <= targetPrice)
                        {

                            x.Status = OrderStatus.Open;
                            x.OrderPriceTriggerd = btcDecimal * 1.001m;
                            x.OrderTriggerdAt = DateTime.Now;
                            await context.SaveChangesAsync();
                        }
                    }
                    if (x.orderTypeProp == OrderTypeProp.Sell && x.MarketType == MarketType.Order && x.Status == OrderStatus.Pending)
                    {
                        if (btcDecimal <= targetPrice - priceRange && btcDecimal >= targetPrice + priceRange)
                        {
                            x.Status = OrderStatus.Open;
                            x.OrderPriceTriggerd = btcDecimal * 0.999m;
                            x.OrderTriggerdAt = DateTime.Now;


                            await context.SaveChangesAsync();
                            Console.WriteLine($"Order opened at price: {btcDecimal}");
                        }
                        //RTODO second side if price under
                        if (btcDecimal < targetPrice - priceRange)
                        {

                            x.Status = OrderStatus.Open;
                            x.OrderPriceTriggerd = btcDecimal;
                            x.OrderTriggerdAt = DateTime.Now;
                            await context.SaveChangesAsync();
                            Console.WriteLine($"Order closed due to low price: {btcDecimal}");
                        }
                    }

                    if (x.orderTypeProp == OrderTypeProp.Buy && x.MarketType == MarketType.Order && x.Status == OrderStatus.Open)
                    {
                        var pnl = Math.Round(CalculateProfitLossInDollar(
            x.OrderPriceTriggerd ?? 0,
            btcDecimal,
                                x.AmountForOrder,
                                x.Leverage, x.orderTypeProp, x.MarketType, x.Status),
                        1
                            );
                        // Stop-loss activated if the price has hit or dropped below StopLoss
                        if (btcDecimal <= x.StopLoss)
                        {


                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.StopLoss;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;
                            x.OrderPriceTriggerd = btcDecimal;
                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder - Math.Abs(pnl));

                            await context.SaveChangesAsync();

                        }
                        if (btcDecimal >= x.TakeProfit)
                        {


                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.TakeProfit;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder + Math.Abs(pnl));

                            await context.SaveChangesAsync();


                        }

                        var liquidationPriceMaxRange = x.AmountForOrder;
                        decimal liquidationPrice = x.OrderPriceOpened * (1 - 1 / x.Leverage);
                        //liquidation max range is for example 50
                        //if pnl is -50 trade liquidated
                        if (pnl <= -liquidationPriceMaxRange)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.Liquidated;
                            await context.SaveChangesAsync();
                        }

                    }
                    if (x.orderTypeProp == OrderTypeProp.Sell && x.MarketType == MarketType.Order && x.Status == OrderStatus.Open)
                    {
                        var pnl = Math.Round(CalculateProfitLossInDollar(
               x.OrderPriceTriggerd ?? 0, btcDecimal,
                               x.AmountForOrder,
                               x.Leverage, x.orderTypeProp, x.MarketType, x.Status),
                       1
                           );
                        // Stop-loss activated if the price has hit or dropped below StopLoss
                        if (btcDecimal >= x.StopLoss)
                        {


                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.StopLoss;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;
                            x.OrderPriceTriggerd = btcDecimal;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder - Math.Abs(pnl));

                            await context.SaveChangesAsync();
                        }
                        if (btcDecimal <= x.TakeProfit)
                        {

                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.TakeProfit;
                            x.TradeClosedDate = DateTime.Now;
                            x.ProfitInCase = pnl;

                            // Corrected addition to balance
                            EditUserX.Balance += (x.AmountForOrder + Math.Abs(pnl));

                            await context.SaveChangesAsync();


                        }

                        var liquidationPriceMaxRange = x.AmountForOrder;
                        decimal liquidationPrice = x.OrderPriceOpened * (1 - 1 / x.Leverage);
                        //liquidation max range is for example 50
                        //if pnl is -50 trade liquidated
                        if (pnl <= -liquidationPriceMaxRange)
                        {
                            x.OrderPriceClosed = btcDecimal;
                            x.Status = OrderStatus.Liquidated;
                            await context.SaveChangesAsync();
                        }
                    }


                }
            }
        }

        private decimal CalculateProfitLossInDollar(decimal orderPrice, decimal currentPrice, decimal amount, int leverage, OrderTypeProp ordertype, MarketType marketType, OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.Pending)
            {
                return 0.0m;
            }

            // Calculate the effective amount based on leverage
            decimal effectiveAmount = (amount * leverage) / orderPrice;
            decimal leveragedProfitLoss = 0m;

            if (orderStatus == OrderStatus.Open && marketType == MarketType.Market || marketType == MarketType.Order)
            {
                if (ordertype == OrderTypeProp.Buy)
                {
                    // For a long position
                    if (currentPrice > orderPrice)
                    {
                        // Profit calculation
                        leveragedProfitLoss = (currentPrice - orderPrice) * effectiveAmount;
                    }
                    else if (currentPrice < orderPrice)
                    {
                        // Loss calculation
                        leveragedProfitLoss = (orderPrice - currentPrice) * effectiveAmount * -1; // Negative for loss
                    }
                }
                else if (ordertype == OrderTypeProp.Sell)
                {
                    // For a short position
                    if (currentPrice < orderPrice)
                    {
                        // Profit calculation
                        leveragedProfitLoss = (orderPrice - currentPrice) * effectiveAmount;
                    }
                    else if (currentPrice > orderPrice)
                    {
                        // Loss calculation
                        leveragedProfitLoss = (currentPrice - orderPrice) * effectiveAmount * -1; // Negative for loss
                    }
                }
            }

            // Return the profit or loss in the same currency as the asset (e.g., USD)
            return leveragedProfitLoss;
        }
    }
}
