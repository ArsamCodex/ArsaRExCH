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

            var expectedIpAddress = "123.456.789.101";
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
                new BanedCountries { IpAdressToBann = "123" }, // Banned IP (first 3 digits)
                new BanedCountries { IpAdressToBann = "456" }  // Another banned IP
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
    }
}