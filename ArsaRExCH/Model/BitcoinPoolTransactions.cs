using ArsaRExCH.Data;

namespace ArsaRExCH.Model
{
    public class BitcoinPoolTransactions
    {
        public int BitcoinPoolTransactionsId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // e.g., Deposit, Withdrawal
        public string TxHash { get; set; }
        public string receiverAdress { get; set; }

    }
}
