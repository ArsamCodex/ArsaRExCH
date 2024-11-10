using ArsaRExCH.Model;
using ArsaRExCH.Model.Prop;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NBitcoin.Secp256k1;
namespace ArsaRExCH.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {


        public DbSet<PropTrade> propTrdaes { get; set; }
        public DbSet<PropUser> propUsers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TransferBetweenAcounts> transferBetweenAcounts { get; set; }

        
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pair> Pair { get; set; }
        public DbSet<BitcoinPool> BitcoinPools { get; set; }
        public DbSet<BitcoinPoolTransactions> poolTransactions { get; set; }
        public DbSet<BanedCountries> BanedCountris { get; set; }
        public DbSet<AirDropFaq> airDropFaqs { get; set; }
        public DbSet<UserDatesRecord> UserDatesRecords { get; set; }
        public DbSet<AdminWarningMessage> adminWarningMessages { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<LiveChat> lifeChat { get; set; }
        public DbSet<TradeFee> tradeFees { get; set; }
        public DbSet<Bet> Bet { get; set; }
        public DbSet<AirDrop> AirDrops { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<AdminSetupInit> adminSetupInits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
      .HasOne(w => w.User)
      .WithMany(u => u.Wallets)
      .HasForeignKey(w => w.ApplicationUserId)
      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reply>()
               .HasOne(r => r.Post)
               .WithMany(p => p.Replies)
               .HasForeignKey(r => r.PostId)
               .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            // Disable cascading delete for Post -> ApplicationUser (Admin)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Admin)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict); // Or use DeleteBehavior.NoAction
            modelBuilder.Entity<AdminSetupInit>()
            .Property(a => a.ShowAdminSetupPopUp)
            .HasDefaultValue(true);


            modelBuilder.Entity<PropTrade>(entity =>
            {
              

                entity.Property(e => e.OrderPriceOpened)
                      .HasPrecision(18, 8);

                entity.Property(e => e.OrderPriceClosed)
                      .HasPrecision(18, 8);

                entity.Property(e => e.ProfitInCase)
                      .HasPrecision(18, 8);

                entity.Property(e => e.FeeInCase)
                      .HasPrecision(18, 8);

                entity.Property(e => e.LiquidationPrice)
                      .HasPrecision(18, 8);

                entity.Property(e => e.StopLoss)
                      .HasPrecision(18, 8);

                entity.Property(e => e.TakeProfit)
                      .HasPrecision(18, 8);

            
            });
            modelBuilder.Entity<PropUser>(entity =>
            {
                entity.Property(e => e.Balance)
                      .HasPrecision(18, 0); // Set precision and scale as needed
            });

            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<ApplicationUser>()
           .HasMany(a => a.Wallets)
           .WithOne(w => w.User)
           .HasForeignKey(w => w.UserID)
           .OnDelete(DeleteBehavior.Cascade);
            */
            /*
            modelBuilder.Entity<Bet>()
           .HasOne(b => b.User)
           .WithMany(u => u.Bets)
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Cascade);
            */


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
                NormalizedUserName = "ARMINTTWAT@GMAIL.COM",
                Email = "ARMINTTWAT@GMAIL.COM",
                NormalizedEmail = "ARMINTTWAT@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", // Replace this with the hashed password
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
                 NetworkName = "BNB"
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
