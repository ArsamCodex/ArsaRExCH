using ArsaRExCH.Data;
using ArsaRExCH.Model;
using ArsaRExCH.Interface;
using NBitcoin;
using Nethereum.HdWallet;
using Wallet = ArsaRExCH.Model.Wallet;
using NBitcoin.DataEncoders;
using Nethereum.Util;

namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface
    {
        private readonly ApplicationDbContext _context;

        public WalletInterfaceIMPL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateBNBWallet(string id)
        {
            Key privateKey = new Key();
            string privateKeyHex = privateKey.ToHex();

            // Derive the public key from the private key
            PubKey publicKey = privateKey.PubKey;
            string publicKeyHex = publicKey.ToHex();

            // Generate the BSC address (Binance Smart Chain uses the same address format as Ethereum)
            var sha3 = new Sha3Keccack();
            var addressBytes = sha3.CalculateHash(publicKey.ToBytes()).Skip(12).ToArray();
            string address = "0x" + Encoders.Hex.EncodeData(addressBytes);

            // Generate a mnemonic (seed phrase)
            Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
            string seedPhrase = mnemo.ToString();
            string[] seedPhraseArray = mnemo.Words;

            // Generate a private key from the mnemonic
            ExtKey hdRoot = mnemo.DeriveExtKey();
            KeyPath keyPath = new KeyPath("m/44'/60'/0'/0/0");
            ExtKey keyPathExtKey = hdRoot.Derive(keyPath);
            Key derivedPrivateKey = keyPathExtKey.PrivateKey;
            string derivedPrivateKeyHex = derivedPrivateKey.ToHex();

            // Derive the address from the derived private key
            PubKey derivedPublicKey = derivedPrivateKey.PubKey;
            var derivedAddressBytes = sha3.CalculateHash(derivedPublicKey.ToBytes()).Skip(12).ToArray();
            string derivedAddress = "0x" + Encoders.Hex.EncodeData(derivedAddressBytes);
            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = "BNB",
                Adress = address,
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
            };


            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return publicKey.ToString();

        }

        public async Task<string> CreateBTCWallet(string id)
        {
            Mnemonic mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            string[] seedPhraseArray = mnemonic.Words;

            // Derive the master extended key from the seed phrase
            ExtKey masterExtendedKey = mnemonic.DeriveExtKey();
            ExtKey derivedKey = masterExtendedKey.Derive(new KeyPath("m/44'/0'/0'/0/0"));

            // Get the private key in WIF format
            string privateKeyWif = derivedKey.PrivateKey.GetWif(Network.Main).ToString();

            // Derive the first Bitcoin address from the derived key
            BitcoinAddress address = derivedKey.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);

            // Print the mnemonic (seed phrase), private key, and address
            Console.WriteLine("Seed Phrase: " + string.Join(" ", seedPhraseArray));
            Console.WriteLine("Private Key (WIF): " + privateKeyWif);
            Console.WriteLine("Address: " + address.ToString());

            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = "BTC",
                Adress = address.ToString(),
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
            };

            // Save the wallet entity to the database
            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return address.ToString();
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
