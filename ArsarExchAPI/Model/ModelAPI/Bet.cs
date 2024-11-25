namespace ArsarExchAPI.Model.ModelAPI

{
    public class Bet
    {
        /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int BetId { get; set; }
        /*I use this Id when I work for self less relations just direct
         But now i made relation just in case */
        public string? UserIdSec { get; set; }
        public double BtcPriceNow { get; set; }
        public double BtcPriceExpireBet { get; set; }
        public DateTime HitDateBTC { get; set; }
        public double BetAmountBtc { get; set; }
        public double EthPriceNow { get; set; }
        public DateTime HitDateETH { get; set; }
        public double EthPriceExpireBet { get; set; }
        public double BetAmountETH { get; set; }
        public double? WiningAmount { get; set; }
        public double? WiningAmountEth { get; set; }
        public bool IsBetActive { get; set; }
        /*I use IsDeleted for delete in shadow we dont want to remove data but
         * in the case of delete operation this bool changed to True otherwise 
         * its False*/
        public bool ISDeleted { get; set; }
        /*when the bet expires*/
        public DateTime CompletedTime { get; set; }
        public DateTime? OpendBetAtricle { get; set; }
        // public ApplicationUser User { get; set; }

        //Generate unique string for eaach bet
        public string BetSignutare { get; set; }
    }
}
