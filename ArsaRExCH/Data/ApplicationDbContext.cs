using ArsaRExCH.Model;
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


    }
}
