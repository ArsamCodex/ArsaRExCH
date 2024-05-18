namespace ArsaRExCH.Model
{
    public class UserTradeActivity
    {
        public int UserTradeActivityID { get; set; }
        public int UserID { get; set; }
        public string PairName { get; set; }
        public double CurrentBalance { get; set; }
    }
}
