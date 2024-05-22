namespace ArsaRExCH.Interface
{
    public interface WalletInterface
    {
        public Task<string> CreateBTCWallet(string id, string PairName);
        public Task<string> CreateETHWallet(string id,string PairName);
        public Task<string> CreateBNBWallet(string id, string PairName);
        public Task<decimal> GetBalanceFromBlockCypherAsync(string bitcoinAddress);
        public Task CheckAndCreateWallets(string userID);
        Task<string> CreateETHNetworksWallet(string id, string PairName);

    }
}
