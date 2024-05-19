namespace ArsaRExCH.Interface
{
    public interface WalletInterface
    {
        public Task CreateBTCWallet(string id);
        public Task CreateETHWallet(string id);
        public Task CreateBNBWallet(string id);
    }
}
