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

      
    }
}