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

     
            [Fact]
            public async Task GetPublicIpAddress_ShouldReturnExpectedIp()
            {
                // Arrange
                var dbContextFactory = CreateDbContextFactory(); // Create the DbContextFactory
                var ipService = new UserInterfaceIMPL(dbContextFactory); // Use real implementation
                var expectedIpAddress = "123.456.789.101";

                // Act
                var result = await ipService.GetPublicIpAddress();

                // Assert
                Assert.Equal(expectedIpAddress, result);
            }

            // Helper method to create a real IDbContextFactory
            private IDbContextFactory<ApplicationDbContext> CreateDbContextFactory()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseSqlServer("DefaultConnection") // Use real SQL Server or SQLite connection
                              .Options;

                return new DbContextFactory(options);
            }

            // Real implementation of IDbContextFactory
            public class DbContextFactory : IDbContextFactory<ApplicationDbContext>
            {
                private readonly DbContextOptions<ApplicationDbContext> _options;
                public DbContextFactory(DbContextOptions<ApplicationDbContext> options)
                {
                    _options = options;
                }

            public ApplicationDbContext CreateDbContext()
            {
                return new ApplicationDbContext(_options);
            }
        }
        
    }
}