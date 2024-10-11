using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ArsaRExCH.Interface;
using ArsaRExCH.InterfaceIMPL;
using ArsaRExCH.Data;
using Microsoft.EntityFrameworkCore;
using ArsaRExCH.Model;
namespace ArsarExTest
{
    public class UserInterfaceTest
    {


        // In-memory database factory for tests
        private class InMemoryDbContextFactory : IDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("TestDatabase") // Use in-memory database for testing
                    .Options;

                return new ApplicationDbContext(options);
            }
        }

        [Fact]
        public async Task GetPublicIpAddress_ShouldReturnExpectedIp()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            using var context = dbContextFactory.CreateDbContext(); // Create a new context instance
            var ipService = new UserInterfaceIMPL( dbContextFactory); // Pass both context and factory

            var expectedIpAddress = "123.456.789.101";  //Change to expected ip adrress
            // Act
            var result = await ipService.GetPublicIpAddress();

            // Assert
            Assert.Equal(expectedIpAddress, result);
        }
        [Fact]
        public async Task CheckIfIpIsBanned_ShouldReturnBannedMessage_WhenIpIsBanned()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            using var context = dbContextFactory.CreateDbContext();

            // Adding test data to the BanedCountris table
            context.BanedCountris.AddRange(new List<BanedCountries>
            {
                new BanedCountries { IpAdressToBann = "123",CountryName= "AFG" }, // Banned IP (first 3 digits)
                new BanedCountries { IpAdressToBann = "456" ,CountryName= "IRQ" }  // Another banned IP
            });
            await context.SaveChangesAsync();

            var ipService = new UserInterfaceIMPL(dbContextFactory);
            string testUserIp = "123.456.789.101"; // Test IP where first part matches banned IP

            // Act
            var result = await ipService.CheckIfIpIsBanned(testUserIp);

            // Assert
            Assert.Equal("You are banned from accessing this service.", result);
        }
        [Fact]
        public async Task CheckIfIpIsBanned_ShouldReturnEmpty_WhenIpIsNotBanned()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            using var context = dbContextFactory.CreateDbContext();

            // Adding test data to the BanedCountris table
            context.BanedCountris.AddRange(new List<BanedCountries>
            {
                new BanedCountries { IpAdressToBann = "123" }, // Banned IP (first 3 digits)
                new BanedCountries { IpAdressToBann = "456" }  // Another banned IP
            });
            await context.SaveChangesAsync();

            var ipService = new UserInterfaceIMPL(dbContextFactory);
            string testUserIp = "789.456.123.101"; // Test IP where first part does not match banned IP

            // Act
            var result = await ipService.CheckIfIpIsBanned(testUserIp);

            // Assert
            Assert.Equal(string.Empty, result); // Should return empty if not banned
        }
        [Fact]
        public async Task CalculateMaxInzetAsync_ShouldReturnCorrectResults()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            using var context = dbContextFactory.CreateDbContext(); // Create a new context instance

            // Insert test data for bets
            context.Bet.AddRange(
                new Bet
                {
                    HitDateBTC = new DateTime(2024, 10, 10),
                    BtcPriceExpireBet = 50000,
                    BetAmountBtc = 2.5,
                    HitDateETH = new DateTime(2024, 10, 10),
                    EthPriceExpireBet = 3000,
                    BetAmountETH = 10,
                    IsBetActive = true,
                    ISDeleted = false,
                    BetSignutare = "sdsdsdsdsd"

                },
                new Bet
                {
                    HitDateBTC = new DateTime(2024, 10, 10),
                    BtcPriceExpireBet = 50000,
                    BetAmountBtc = 1.5,
                    HitDateETH = new DateTime(2024, 10, 10),
                    EthPriceExpireBet = 3000,
                    BetAmountETH = 5,
                    IsBetActive = true,
                    ISDeleted = false,
                    BetSignutare = "sdsdsdsdsdasass"

                },
                new Bet
                {
                    HitDateBTC = new DateTime(2024, 10, 12),
                    BtcPriceExpireBet = 60000,
                    BetAmountBtc = 3.5,
                    HitDateETH = new DateTime(2024, 10, 12),
                    EthPriceExpireBet = 3200,
                    BetAmountETH = 7,
                    IsBetActive = true,
                    ISDeleted = false,
                    BetSignutare = "sd454rersdsdsdsd"

                }
            );

            // Save changes to the in-memory database
            await context.SaveChangesAsync();

            // Instantiate the service with the in-memory DbContextFactory
            var service = new UserInterfaceIMPL(dbContextFactory);

            // Act
            var result = await service.CalculateMaxInzetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Expecting two unique dates (2024-10-10 and 2024-10-12)

            // Check the results for the first date (2024-10-10)
            var firstResult = result.First(r => r.MyDateTime == new DateTime(2024, 10, 10));
            Assert.Equal(50000, firstResult.HitBtcPrice);
            Assert.Equal(4.0, firstResult.MaxBtcTotalInBTC); // 2.5 + 1.5 = 4.0 BTC total
            Assert.Equal(3000, firstResult.HitEthPrice);
            Assert.Equal(15.0, firstResult.MaxEthTotal); // 10 + 5 = 15 ETH total

            // Check the results for the second date (2024-10-12)
            var secondResult = result.First(r => r.MyDateTime == new DateTime(2024, 10, 12));
            Assert.Equal(60000, secondResult.HitBtcPrice);
            Assert.Equal(3.0, secondResult.MaxBtcTotalInBTC); // 3.0 BTC total
            Assert.Equal(3200, secondResult.HitEthPrice);
            Assert.Equal(7.5, secondResult.MaxEthTotal); // 7.5 ETH total
        }

    }
}