using ArsaRExCH.Data;
using ArsaRExCH.Model;
using ArsaRExCH.Interface;
using NBitcoin;
using Nethereum.HdWallet;

namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface
    {
        private readonly ApplicationDbContext _context;

        public WalletInterfaceIMPL(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateBNBWallet(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateBTCWallet(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateETHWallet(string id)
        {
            var wallet = new Nethereum.HdWallet.Wallet(Wordlist.English, WordCount.Twelve);
            var seedPhrase = wallet.Words;
            var account = wallet.GetAccount(0);

            // Create a new wallet entity
            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = "ETH",
                Adress = account.Address,
                Amount = 0,
                SeedPhrase = seedPhrase,
                CurrentPrice = 0,
            };

            // Save the wallet entity to the database
            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return account.Address;
        }
    }
}
