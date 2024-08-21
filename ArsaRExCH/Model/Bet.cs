using Org.BouncyCastle.Utilities.IO.Pem;

namespace ArsaRExCH.Model
{
    public class Bet
    {
        /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int BetId { get; set; }
        public string UserId { get; set; }
        public double BtcPrice { get; set; }
        public DateTime HitDate { get; set; }
        public double EthPrice { get; set; }
        public DateTime HitDateII { get; set; }
        public double BetAmount { get; set; }
        public double? WiningAmount { get; set; }
        public bool IsBetActive { get; set; }
        /*I use IsDeleted for delete in shadow we dont want to remove data but
         * in the case of delete operation this bool changed to True otherwise 
         * its False*/
        public bool ISDeleted { get; set; }

    }
}
