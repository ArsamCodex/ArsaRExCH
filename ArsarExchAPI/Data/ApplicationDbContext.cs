using ArsaRExCH.Model.Prop;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArsarExchAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        /*
        public DbSet<PropTrade> propTrdaes { get; set; }
        public DbSet<PropUser> propUsers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TransferBetweenAcounts> transferBetweenAcounts { get; set; }
        public DbSet<PrepPir> prepPirs { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        
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
        public DbSet<AdminSetupInit> adminSetupInits { get; set; }*/
        public DbSet<Pair> Pair { get; set; }
    }
}
