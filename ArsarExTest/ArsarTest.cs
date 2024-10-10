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
namespace ArsarExTest
{
    public class ArsarTest
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
    }
}