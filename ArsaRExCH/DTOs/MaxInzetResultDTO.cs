namespace ArsaRExCH.DTOs
{
    public class MaxInzetResultDTO
    {
        public DateTime MyDateTime { get; set; }
        // on which price bets are placed
        public double HitBtcPrice { get; set; }
        //total of all betz on that btc price
        public double MaxBtcTotalInBTC { get; set; }
        public double HitEthPrice { get; set; }
        public double MaxEthTotal { get; set; }
        public string ErrorMessage { get; set; }
    }
}
