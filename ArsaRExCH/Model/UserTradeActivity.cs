namespace ArsaRExCH.Model
{
    public class UserTradeActivity
    {  /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int UserTradeActivityID { get; set; }
        public int UserID { get; set; }
        public string PairName { get; set; }
        public double CurrentBalance { get; set; }
    }
}
