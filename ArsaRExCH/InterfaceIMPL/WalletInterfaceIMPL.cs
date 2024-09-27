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
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;


namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface<double>
    {
        private readonly ApplicationDbContext _context;
        public readonly HttpClient httpClient;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public WalletInterfaceIMPL(ApplicationDbContext context, HttpClient httpClient, IConfiguration configuration, IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _context = context;
            this.httpClient = httpClient;
            _configuration = configuration;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<string> CreateBNBWallet(string id, string PairName)
        {
            var context = _dbContextFactory.CreateDbContext();
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
                UserIDSec = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = address,
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
                PrivateKey = privateKeyHex,
                Network = "BNB"
            };


            await context.Wallet.AddAsync(walletEntity);
            await context.SaveChangesAsync();
            return publicKey.ToString();

        }

        public async Task<string> CreateBTCWallet(string id, string PairName)
        {
            var context = _dbContextFactory.CreateDbContext();
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
                UserIDSec = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = address.ToString(),
                Amount = 0,
                SeedPhrase = seedPhraseArray,
                CurrentPrice = 0,
                PrivateKey = privateKeyWif,
                Network = "BTC"
            };

            // Save the wallet entity to the database
            await context.Wallet.AddAsync(walletEntity);
            await context.SaveChangesAsync();
            return address.ToString();
        }

        public async Task<string> CreateETHWallet(string id, string PairName)
        {
            var context = _dbContextFactory.CreateDbContext();
            var wallet = new Nethereum.HdWallet.Wallet(Wordlist.English, WordCount.Twelve);
            var seedPhrase = wallet.Words;
            var account = wallet.GetAccount(0);
            var privateKey = account.PrivateKey;
            // Create a new wallet entity
            var walletEntity = new Model.Wallet
            {
                UserIDSec = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = account.Address,
                Amount = 0,
                SeedPhrase = seedPhrase,
                CurrentPrice = 0,
                PrivateKey = privateKey,
                Network = "ETH"
            };

            // Save the wallet entity to the database
            await context.Wallet.AddAsync(walletEntity);
            await context.SaveChangesAsync();
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
                var context = _dbContextFactory.CreateDbContext();
                var pairs = await context.Pair
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
                            var x = await _context.Wallet.FirstOrDefaultAsync(c => c.UserIDSec == userID && c.PairName == "ETH");
                            var ad = x.Adress;
                            var seed = x.SeedPhrase;
                            var privateKey = x.PrivateKey;
                            var walletEntity = new Model.Wallet
                            {
                                UserIDSec = userID, // Replace with actual user ID retrieval logic
                                PairName = pair.PaiName,
                                Adress = ad,
                                Amount = 0,
                                SeedPhrase = seed,
                                CurrentPrice = 0,
                                PrivateKey = privateKey,
                                Network = "ETH"
                            };

                            // Save the wallet entity to the database
                            await context.Wallet.AddAsync(walletEntity);
                            await context.SaveChangesAsync();
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
            var context = _dbContextFactory.CreateDbContext();
            var x = await context.Wallet.FirstOrDefaultAsync(c => c.UserIDSec == id && c.Network == "ETH");
            var ad = x.Adress;
            var seed = x.SeedPhrase;
            var privateKey = x.PrivateKey;
            var walletEntity = new Model.Wallet
            {
                UserIDSec = id, // Replace with actual user ID retrieval logic
                PairName = PairName,
                Adress = ad,
                Amount = 0,
                SeedPhrase = seed,
                CurrentPrice = 0,
                PrivateKey = privateKey,
                Network = "ETH"
            };

            // Save the wallet entity to the database
            await context.Wallet.AddAsync(walletEntity);
            await context.SaveChangesAsync();
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

        public async Task<double> GetBTCBalanceOfWalletAsync(string walletId)
        {
            if (string.IsNullOrEmpty(walletId))
                throw new ArgumentNullException(nameof(walletId));

            string url = $"https://blockchain.info/balance?active={walletId}";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to retrieve balance for wallet {walletId}. Status code: {response.StatusCode}");

            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject balanceData = JObject.Parse(jsonResponse);

            // Extract the balance in satoshis
            long satoshiBalance = balanceData[walletId]["final_balance"].Value<long>();

            // Convert satoshis to BTC
            double btcBalance = satoshiBalance / 1e8;

            //Here when we check blockchain for btc wallet balance Then we check amount in database then edit amount
            ///check last transactions of a wallet for new trwansacstion


            return btcBalance;
        }

        public Task<double> GetBTCBalanceOfWallet(string walletId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WalletDTO>> GetWalletsListed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new List<WalletDTO>(); // Return an empty list if the ID is null or empty
            }

            try
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {
                    var wallets = await context.Wallet
                        .Where(w => w.UserIDSec == id)
                        .Select(w => new WalletDTO
                        {
                            PairName = w.PairName,
                            Adress = w.Adress,
                            CurrentBalance = w.CurrentPrice,
                            Amount = w.Amount,
                            Network = w.Network
                        })
                        .ToListAsync();

                    return wallets;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }



        public async Task<(List<double> BtcBalances, List<double> EthBalances, List<double> BnbBalances)> GetWalletBalancesForUser(string userId)
        {
            // Retrieve all wallets for the given user
            var userWallets = await _context.Wallet
                .Where(c => c.UserIDSec == userId)
                .ToListAsync();

            // Filter and get BTC wallet amounts
            var btcBalances = userWallets
                .Where(c => c.Network == "BTC")
                .Select(c => c.Amount)
                .ToList();

            // Filter and get ETH wallet amounts
            var ethBalances = userWallets
                .Where(c => c.Network == "ETH")
                .Select(c => c.Amount)
                .ToList();

            // Filter and get BNB wallet amounts
            var bnbBalances = userWallets
                .Where(c => c.Network == "BNB")
                .Select(c => c.Amount)
                .ToList();

            // Return amounts for BTC, ETH, and BNB wallets separately
            return (btcBalances, ethBalances, bnbBalances);
        }

        /*
         * WARNING Edit bet onlly when bet place and NOT when u want increasse 
         * This method only decrease the amount wallet.Amount -= amount;
         * in wining possition or increassing we make other method*/
        public async Task<double> EditWalletAmountDecrease(string userID, double amount)
        {
            var context = _dbContextFactory.CreateDbContext();
            // Retrieve the wallet entry for the specified user and currency pair
            var wallet = await context.Wallet
                .Where(c => c.UserIDSec == userID && c.PairName == "BTC")
                .FirstOrDefaultAsync(); // Execute the query and get the first result

            // Check if the wallet entry exists
            if (wallet == null || amount < 0)
            {
                throw new InvalidOperationException("Wallet not found for the specified user.");
            }
            // Update the wallet amount
            //USE THIS METHOD TO EDIT WALLET WHEN BET PLACED
            wallet.Amount -= amount;
            if (wallet.Amount < 0)
            {
                throw new InvalidOperationException("Invalid Bet: Wallet balance cannot be negative.");
            }

            // Save the updated wallet entry to the database
            context.Wallet.Update(wallet);
            await context.SaveChangesAsync();

            // Return the updated amount
            return wallet.Amount;
        }

        public async Task<double> EditWalletAmountIncrease(string userID, double amount)
        {
            /* Here we have now 2 seperate method for edit wallet in both winning and lost position
             * . you can make 1 method fot both also  , but i like this way 
             * */
            var context = _dbContextFactory.CreateDbContext();
            var wallet = await context.Wallet
                .Where(c => c.UserIDSec == userID && c.PairName == "BTC")
                .FirstOrDefaultAsync(); // Execute the query and get the first result

            if (wallet == null || amount < 0)
            {
                throw new InvalidOperationException("Wallet not found for the specified user.");
            }
            // Update the wallet amount
            //USE THIS METHOD TO EDIT WALLET onlly for decrease amount 
            wallet.Amount += amount;

            // Save the updated wallet entry to the database
            context.Wallet.Update(wallet);
            await context.SaveChangesAsync();

            // Return the updated amount
            return wallet.Amount;
        }
        /*
        public async Task<List<UserBetCount>> GetFIrstXwinners()
        {
            try
            {
                var result = await _context.Bet
                    .Where(b => b.WiningAmount!=0) // Filter out deleted or inactive bets
                    .GroupBy(b => b.UserIdSec)
                    .Select(g => new UserBetCount
                    {
                        UserId = g.Key,
                        TotalWinningAmount = g.Sum(b => b.WiningAmount ?? 0) // Sum of winning amounts
                    })
                    .OrderByDescending(x => x.TotalWinningAmount) // Order by total winning amount
                    .ToListAsync();

                if (!result.Any())
                {
                    Console.WriteLine("No results found.");
                }

                foreach (var item in result)
                {
                    Console.WriteLine($"UserId: {item.UserId}, TotalWinningAmount: {item.TotalWinningAmount}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total winning amounts: {ex.Message}");
                return new List<UserBetCount>();
            }
        }*/


        public async Task<List<UserBetCount>> GetFIrstXwinners()
        {
            var topCount = 10;
            try
            {
                // Fetch top X winners
                var result = await _context.Bet
                    .Where(b => b.WiningAmount != 0 && b.WiningAmount !=null) // Filter out irrelevant bets
                    .GroupBy(b => b.UserIdSec)
                    .Select(g => new UserBetCount
                    {
                        UserId = g.Key,
                        TotalWinningAmount = g.Sum(b => b.WiningAmount ?? 0) // Calculate total winning amount
                    })
                    .OrderByDescending(x => x.TotalWinningAmount) // Order by total winnings in descending order
                    .Take(topCount) // Limit to top X users
                    .ToListAsync(); // Execute query

                if (!result.Any())
                {
                    Console.WriteLine("No results found.");
                }

                // Log the results for debugging
                foreach (var item in result)
                {
                    Console.WriteLine($"UserId: {item.UserId}, TotalWinningAmount: {item.TotalWinningAmount}");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching top winners: {ex.Message}");
                return new List<UserBetCount>();
            }
        }

        public async Task<double> EditWalletAmountDecresETH(string userID, double amount)
        {
            using var context = _dbContextFactory.CreateDbContext();
            // Retrieve the wallet entry for the specified user and currency pair
            var wallet = await context.Wallet
                .Where(c => c.UserIDSec == userID && c.PairName == "ETH")
                .FirstOrDefaultAsync(); // Execute the query and get the first result

            // Check if the wallet entry exists
            if (wallet == null || amount < 0)
            {
                throw new InvalidOperationException("Wallet not found for the specified user.");
            }
            // Update the wallet amount
            //USE THIS METHOD TO EDIT WALLET WHEN BET PLACED
            wallet.Amount -= amount;
            if (wallet.Amount < 0)
            {
                throw new InvalidOperationException("Invalid Bet: Wallet balance cannot be negative.");
            }

            // Save the updated wallet entry to the database
            context.Wallet.Update(wallet);
            await context.SaveChangesAsync();

            // Return the updated amount
            return wallet.Amount;
        }
    }

}

