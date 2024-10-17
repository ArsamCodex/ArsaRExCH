using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class Trade
    {
        public string TradeId { get; set; }
        public PairNames TradePair { get; set; }
        public DateTime dateTime { get; set; }
        public double symbolI{ get; set; }
        public double SymbolII { get; set; }
        /*Here trade fee administrator set fee we need other class and othercall to set this value*/
        public double TradeFee { get; set; }
        public bool IsMarketBuy { get; set; }
        public bool IsTradeDone { get; set; }
        public string MyProperty { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BitcoinPoolId { get; set; }
        public BitcoinPool BitcoinPool { get; set; }
    }
}
