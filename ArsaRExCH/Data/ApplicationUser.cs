using ArsaRExCH.Model;
using ArsaRExCH.Model.Prop;
using Microsoft.AspNetCore.Identity;

namespace ArsaRExCH.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
         public List<PropUser> propUsers { get; set; } = new List<PropUser>();
         public List<Wallet> Wallets { get; set; } = new List<Wallet>();
      //  public List<PropTrade> propTrdaes { get; set; } = new List<PropTrade>();
        public DateTime? LastLoginDate { get; set; }
        public List<UserDatesRecord> UserLoginDates { get; set; } = new List<UserDatesRecord>();
        public List<AirDrop> UserAirdops { get; set; } = new List<AirDrop>();
        public ICollection<Post> Posts { get; set; }=new HashSet<Post>();
        public List<Trade> Trade { get; set; } = new List<Trade>();
        public List<LiveChat> liveChats { get; set; } = new List<LiveChat>();




    }

}
