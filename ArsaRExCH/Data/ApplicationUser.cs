using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;

namespace ArsaRExCH.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // public ICollection<Bet> Bets { get; set; }
        // public ICollection<Wallet> Wallets { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

}
