namespace ArsaRExCH.Interface
{
    public interface WalletInterface
    {
        public Task<string> CreateBTCWallet(string id);
        public Task<string> CreateETHWallet(string id);
        public Task<string> CreateBNBWallet(string id);
        public Task<decimal> GetBalanceFromBlockCypherAsync(string bitcoinAddress);
        public Task CheckAndCreateWallets(string userID);
    }
}
