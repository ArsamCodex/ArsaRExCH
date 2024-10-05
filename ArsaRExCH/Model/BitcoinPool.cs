namespace ArsaRExCH.Model
{
    public class BitcoinPool
    {
        public int BitcoinPoolId { get; set; }
        public string PoolName { get; set; }
        public double PoolCurrentBalance { get; set; }
        public bool IsPoolActive { get; set; } = true;
        /* If IsPoolActive is false then SuspendDate gets initialized*/
        public DateTime? SuspendDate { get; set; }
        /*Date when a pool get rich by admin*/
        public DateTime? Daterefilled { get; set; }
        /*track adminID*/
        public string AdminUserId { get; set; }
        public DateTime? PoolOpenedDate { get; set; }
    }
}
