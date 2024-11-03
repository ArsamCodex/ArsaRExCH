using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;

namespace ArsaRExCH.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // public ICollection<Bet> Bets { get; set; }
         public List<Wallet> Wallets { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public List<UserDatesRecord> UserLoginDates { get; set; } = new List<UserDatesRecord>();
        public List<AirDrop> UserAirdops { get; set; } = new List<AirDrop>();
        public ICollection<Post> Posts { get; set; }
        public List<Trade> Trade { get; set; }
        public List<LiveChat> liveChats { get; set; } = new List<LiveChat>();




    }

}
