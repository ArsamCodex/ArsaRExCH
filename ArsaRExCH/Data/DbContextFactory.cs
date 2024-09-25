using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Data
{
    public class DbContextFactory
    {
        /*Manual implementation of dbcontext factory every time new instance 
         * I Also Add IdbcontextFactory if you like to use , i wil also use in other method to use both 
         */
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
