namespace ArsaRExCH.Model
{
    public class Wallet
    {
        public int WalletID { get; set; }
        public string PairName { get; set; }
        public string Adress { get; set; }
        public string UserID { get; set; }
        public double CurrentPrice { get; set; }
        public double Amount { get; set; }
        public string[] SeedPhrase { get; set; }
    }
}
