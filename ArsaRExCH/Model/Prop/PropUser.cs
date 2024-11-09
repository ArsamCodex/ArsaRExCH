using ArsaRExCH.Data;

namespace ArsaRExCH.Model.Prop
{
    public class PropUser
    {
        public int PropUserId { get; set; }
        public decimal Balance { get; set; }
        public AccountType CurrentAccountType { get; set; }
        public bool IsAccountActive { get; set; }

        // Initialize list to prevent null issues
        public List<PropTrade> PropTrades { get; set; } = new List<PropTrade>();
        public string ApplicationUserId { get; set; }
         public ApplicationUser ApplicationUser { get; set; }
        public bool IsTermAndConditionAccepted { get; set; }
    }
}
