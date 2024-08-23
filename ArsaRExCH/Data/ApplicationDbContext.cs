using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<UserClient> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pair> Pair { get; set; }
        public DbSet<UserTradeActivity> UserTradeActivities { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Bet> Bet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedInitialData(modelBuilder);
            SeedInitialWalletData(modelBuilder);


        }
        /*
         Admin Account has been made whitout wallet adresess . administration has no 
        right to trade in own web page */
        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // Seed default role
            const string defaultRoleName = "Admin";
            var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = defaultRoleName, NormalizedName = defaultRoleName.ToUpper() };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Seed new user
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "arminttwat@gmail.com",
                NormalizedUserName = "arminttwat@gmail.com",
                Email = "arminttwat@gmail.com",
                NormalizedEmail = "NEWUSER@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEDiy5mMJAzNnerdU6G5JpACSOMq93YVj+PV1BgLNtsE3o0Lihn4AkClNHXNO7KV/X==", // Replace this with the hashed password
                SecurityStamp = Guid.NewGuid().ToString()
            };
            modelBuilder.Entity<ApplicationUser>().HasData(newUser);

            // Assign default role to the new user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = newUser.Id
            });
        }
 
        private void SeedInitialWalletData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pair>().HasData(
             new Pair
             {
                 PairID = 1,
                 PaiName = "BTC",
                 ListPrice = 100.0,
                 ListedDate = DateTime.Now,
                 NetworkName = "BTC"
             },
             new Pair
             {
                 PairID = 2,
                 PaiName = "BNB",
                 ListPrice = 200.0,
                 ListedDate = DateTime.Now,
                 NetworkName = "BTC"
             },
             new Pair
             {
                 PairID = 3,
                 PaiName = "ETH",
                 ListPrice = 300.0,
                 ListedDate = DateTime.Now,
                 NetworkName = "ETH"
             }
         );
        }
    }
}
