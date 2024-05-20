namespace ArsaRExCH.Interface
{
    public interface WalletInterface
    {
        public Task CreateBTCWallet(string id);
        public Task<string> CreateETHWallet(string id);
        public Task<string> CreateBNBWallet(string id);
    }
}
