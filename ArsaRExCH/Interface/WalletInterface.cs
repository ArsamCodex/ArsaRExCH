namespace ArsaRExCH.Interface
{
    public interface WalletInterface
    {
        public Task<string> CreateBTCWallet(string id);
        public Task<string> CreateETHWallet(string id);
        public Task<string> CreateBNBWallet(string id);
    }
}
