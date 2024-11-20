namespace ArsaRExCH.Model
{
    public class TradeFee
    {
        public string TradeFeeId { get; set; }
        /*Fee in decimal Procentage   */
        public double FeeInBtc { get; set; }
        public string SetByAdminId { get; set; }
        public string BitcoinWalletExchange { get; set; }
    }
}
