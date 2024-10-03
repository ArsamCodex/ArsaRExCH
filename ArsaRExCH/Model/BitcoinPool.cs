namespace ArsaRExCH.Model
{
    public class BitcoinPool
    {
        public int BitcoinPoolId { get; set; }
        public string PoolName { get; set; }
        public double PoolCurrentBalance { get; set; }
        public List<BitcoinPoolTransactions> bitcoinPoolTransactions { get; set; }
    }
}
