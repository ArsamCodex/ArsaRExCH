using ArsaRExCH.Data;
using Microsoft.AspNetCore.Identity;

namespace ArsaRExCH.Model
{
    public class Wallet
    {  /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int WalletID { get; set; }
        public string PairName { get; set; }
        public string Adress { get; set; }
        public string UserIDSec { get; set; }
        public double CurrentPrice { get; set; }
        public double Amount { get; set; }
        public string[] SeedPhrase { get; set; }
        public string PrivateKey { get; set; }
        public string Network { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
