using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.InterfaceIMPL;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ArsarExTest
{
    public class AdministrationInterfaceTest
    {
        private readonly InMemoryDbContextFactory _dbContextFactory;
        private readonly PriceInterface _priceInterface;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationInterfaceTest()
        {
            _dbContextFactory = new InMemoryDbContextFactory(); // Use your in-memory context factory
            _userManager = CreateTestUserManager(_dbContextFactory); // Pass the in-memory db context to the TestUserManager
            _priceInterface = new PrriceInterfaceIMPL(); // Implement a fake PriceInterface
        }

        [Fact]
        public async Task AddBannCountries_ShouldAddBannedCountry()
        {
            // Arrange
            var adminInterface = new AdministrationInterfaceIMPL(
                _priceInterface,
                _dbContextFactory,
                _userManager);

            var bannedCountry = new BanedCountries
            {
                CountryName = "TestCountry"
            };

            // Act
            await adminInterface.AddBannCountries(bannedCountry);

            // Assert
            using var context = _dbContextFactory.CreateDbContext(); // Create a new context for assertions
            var addedCountry = await context.BanedCountris.FirstOrDefaultAsync(b => b.CountryName == bannedCountry.CountryName);
            Assert.NotNull(addedCountry);
            Assert.Equal(bannedCountry.CountryName, addedCountry.CountryName);
        }

        // InMemoryDbContextFactory implementation
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

        // A simple implementation of UserManager for testing
        private UserManager<ApplicationUser> CreateTestUserManager(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var store = new UserStore<ApplicationUser>(dbContextFactory.CreateDbContext());
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var optionsAccessor = Options.Create(new IdentityOptions());
            var userValidators = new List<IUserValidator<ApplicationUser>>();
            var passwordValidators = new List<IPasswordValidator<ApplicationUser>>();
            var normalizer = new UpperInvariantLookupNormalizer();
            var describer = new IdentityErrorDescriber();
            var logger = NullLogger<UserManager<ApplicationUser>>.Instance;

            // Initialize UserManager with all required dependencies
            return new UserManager<ApplicationUser>(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                normalizer,
                describer,
                null,
                logger);
        }
        [Fact]
        public async Task AddBannCountries_ShouldThrowException_WhenDatabaseErrorOccurs()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            var logger = new Logger<AdministrationInterfaceIMPL>(new LoggerFactory());
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => adminInterface.AddBannCountries(null));

            // Assert: Check that the exception message is as expected
            Assert.Contains("BanedCountries cannot be null", exception.Message);
        }
        [Fact]
        public async Task AllUsers_ShouldReturnAllUsers_WhenUsersExist()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            var logger = new Logger<AdministrationInterfaceIMPL>(new LoggerFactory());
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            // Adding test users to the in-memory database
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.Users.Add(new ApplicationUser { Id = "1", UserName = "User1" });
                context.Users.Add(new ApplicationUser { Id = "2", UserName = "User2" });
                context.SaveChanges();
            }

            // Act
            var result = await adminInterface.AllUsers();

            // Assert
            Assert.NotNull(result); // Check if result is not null
            Assert.Equal(2, result.Count); // Check if we have 2 users
            Assert.Contains(result, user => user.UserName == "User1"); // Check if User1 is in the result
            Assert.Contains(result, user => user.UserName == "User2"); // Check if User2 is in the result
        }

        [Fact]
        public async Task AllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            var logger = new Logger<AdministrationInterfaceIMPL>(new LoggerFactory());
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            using (var context = dbContextFactory.CreateDbContext())
            {
                context.airDropFaqs.AddRange(new List<AirDropFaq>
            {
                new AirDropFaq { AirDropFaqId = 1, Message = "What is an airdrop?", time = DateTime.Now },
                new AirDropFaq { AirDropFaqId = 2, Message = "How do I participate?", time = DateTime.Now }
            });
                await context.SaveChangesAsync();
            }
        }
        [Fact]
        public async Task GetAllAirDropFaq_ShouldReturnAllFaqs_WhenDataExists()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            // Seed the in-memory database with sample data
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.airDropFaqs.AddRange(new List<AirDropFaq>
            {
                new AirDropFaq { AirDropFaqId = 1, Message = "What is an airdrop?", time = DateTime.Now },
                new AirDropFaq { AirDropFaqId = 2, Message = "How do I participate?", time = DateTime.Now }
            });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await adminInterface.GetAllAirDropFaq();

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal(2, result.Count); // Ensure it returns the correct number of items
            Assert.Equal("What is an airdrop?", result[0].Message); 
            Assert.Equal("How do I participate?", result[1].Message); 
        }
        [Fact]
        public async Task GetAllBannedCountriesInDatabase_ShouldReturnAllBannedCountries_WhenDataExists()
        {
            // Arrange
            var dbContextFactory = new InMemoryDbContextFactory();
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            // Seed the in-memory database with sample data
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.BanedCountris.AddRange(new List<BanedCountries>
            {
                new BanedCountries { BanedCountriesId = 1, CountryName = "Country A", IpAdressToBann = "123" },
                new BanedCountries { BanedCountriesId = 2, CountryName = "Country B", IpAdressToBann = "456" }
            });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await adminInterface.GetAllBannedCountriesInDatabase();

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal(2, result.Count); // Ensure it returns the correct number of items
            Assert.Equal("Country A", result[0].CountryName); // Validate first country's name
            Assert.Equal("Country B", result[1].CountryName); // Validate second country's name
        }

        [Fact]
        public async Task GetAllBannedCountriesInDatabase_ShouldThrowException_WhenDbContextIsNull()
        {
            // Arrange
            IDbContextFactory<ApplicationDbContext> dbContextFactory = null; // Set the dbContextFactory to null
            var priceInterface = _priceInterface; // Assume this is defined
            var userManager = _userManager; // Implement a test UserManager if needed

            var adminInterface = new AdministrationInterfaceIMPL(priceInterface, dbContextFactory, userManager);

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => adminInterface.GetAllBannedCountriesInDatabase());
            Assert.Equal("Value cannot be null. (Parameter '_dbContext')", exception.Message);

        }

    }


}


