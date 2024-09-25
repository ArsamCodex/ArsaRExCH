using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Data
{
    public class DbContextFactory
    {
        private readonly string _connectionString;

        public DbContextFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
