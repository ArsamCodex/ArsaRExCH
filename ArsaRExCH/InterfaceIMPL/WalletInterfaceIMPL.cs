using ArsaRExCH.Data;
using ArsaRExCH.Model;
using ArsaRExCH.Interface;
using NBitcoin;
using Nethereum.HdWallet;
using Wallet = ArsaRExCH.Model.Wallet;
using NBitcoin.DataEncoders;
using Nethereum.Util;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using ArsaRExCH.DTOs;
using System.Net.Mail;
using System.Net;


namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface
    {
        private readonly ApplicationDbContext _context;
        public readonly HttpClient httpClient;
        private readonly IConfiguration _configuration;

        public WalletInterfaceIMPL(ApplicationDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            this.httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> CreateBNBWallet(string id, string PairName)
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
                PairName = PairName,
                Adress = address,
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
                PrivateKey = privateKeyHex,
                Network = "BNB"
            };


            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return publicKey.ToString();

        }

        public async Task<string> CreateBTCWallet(string id, string PairName)
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
                PairName = PairName,
                Adress = address.ToString(),
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
                PrivateKey = privateKeyWif,
                Network = "BTC"
            };

            // Save the wallet entity to the database
            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return address.ToString();
        }

        public async Task<string> CreateETHWallet(string id, string PairName)
        {
            var wallet = new Nethereum.HdWallet.Wallet(Wordlist.English, WordCount.Twelve);
            var seedPhrase = wallet.Words;
            var account = wallet.GetAccount(0);
            var privateKey = account.PrivateKey;
            // Create a new wallet entity
            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = account.Address,
                Amount = 0,
                SeedPhrase = seedPhrase,
                CurrentPrice = 0,
                PrivateKey = privateKey,
                Network = "ETH"
            };

            // Save the wallet entity to the database
            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return account.Address;
        }

        /*
         * Here to me this is NOT what i like, because of third party data . proper way to do run own 
         * bitcoin node and get data from there . This is aso practical and can be done , personaly
         * i dont like this approach and i dont have bitcoin node at the time localay
         * */
        public async Task<decimal> GetBalanceFromBlockCypherAsync(string bitcoinAddress)
        {
            var url = $"https://api.blockcypher.com/v1/btc/main/addrs/{bitcoinAddress}/balance";

            var response = await httpClient.GetFromJsonAsync<BlockCypherBalanceResponse>(url);

            if (response != null)
            {
                // BlockCypher returns balance in satoshis, convert it to BTC
                return SatoshisToBitcoin(response.balance);
            }

            return 0;
        }
        private decimal SatoshisToBitcoin(long satoshis)
        {
            return satoshis / 100_000_000m;
        }
        /*
         * use this 3 wallet as default for every user when registered
         * */
        public async Task CheckAndCreateWallets(string userID)
        {
            try
            {
                var pairs = await _context.Pair
                      .Select(p => new { p.PaiName, p.NetworkName })
                      .ToListAsync();

                foreach (var pair in pairs)
                {
                    if (pair.NetworkName == "BTC")
                    {
                        await CreateBTCWallet(userID, pair.PaiName);
                    }
                    if (pair.NetworkName == "ETH")
                    {
                        if (pair.PaiName == "ETH")
                        {
                            await CreateETHWallet(userID, pair.PaiName);
                        }
                        else
                        {
                            var x = await _context.Wallet.FirstOrDefaultAsync(c => c.UserID == userID && c.PairName == "ETH");
                            var ad = x.Adress;
                            var seed = x.SeedPhrase;
                            var privateKey = x.PrivateKey;
                            var walletEntity = new Model.Wallet
                            {
                                UserID = userID, // Replace with actual user ID retrieval logic
                                PairName = pair.PaiName,
                                Adress = ad,
                                Amount = 0,
                                SeedPhrase = seed,
                                CurrentPrice = 0,
                                PrivateKey = privateKey,
                                Network = "ETH"
                            };

                            // Save the wallet entity to the database
                            await _context.Wallet.AddAsync(walletEntity);
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (pair.NetworkName == "BNB")
                    {
                        await CreateBNBWallet(userID, pair.PaiName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private class BlockCypherBalanceResponse
        {
            public long balance { get; set; }
        }


        public async Task<string> CreateETHNetworksWallet(string id, string PairName)
        {
            var x = await _context.Wallet.FirstOrDefaultAsync(c => c.UserID == id && c.Network == "ETH");
            var ad = x.Adress;
            var seed = x.SeedPhrase;
            var privateKey = x.PrivateKey;
            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = ad,
                Amount = 0,
                SeedPhrase = seed,
                CurrentPrice = 0,
                PrivateKey = privateKey,
                Network = "ETH"
            };

            // Save the wallet entity to the database
            await _context.Wallet.AddAsync(walletEntity);
            await _context.SaveChangesAsync();
            return PairName;
        }


        public Task SendEmailAsync(EmailRequest request)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var mail = emailSettings["EmailAddress"];
            var pass = emailSettings["Password"];
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pass)
            };
            return client.SendMailAsync(

                new MailMessage(from: mail,
                to: request.Email,
                request.Subject,
                request.HtmlMessage



               ));
        }

    }
}
