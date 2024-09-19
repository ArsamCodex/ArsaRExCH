using ArsaRExCH.Model;

namespace ArsaRExCH.DTOs
{
    public class UserAnalyticsDTO
    {
        public int TradeCount { get; set; }
        public int WinCOunt { get; set; }
        public int LossCount { get; set; }
        public double TottalWIn { get; set; }
        public double TottalWInEth { get; set; }

        public double TottalInzet { get; set; }
        public string PairName { get; set; }
        public List<Bet> WiningBets { get; set; }
    }
}
