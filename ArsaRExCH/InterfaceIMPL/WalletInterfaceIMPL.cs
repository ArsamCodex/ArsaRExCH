﻿using ArsaRExCH.Data;
using ArsaRExCH.Model;
using ArsaRExCH.Interface;
using NBitcoin;
using Nethereum.HdWallet;
using Wallet = ArsaRExCH.Model.Wallet;
using NBitcoin.DataEncoders;
using Nethereum.Util;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface
    {
        private readonly ApplicationDbContext _context;
        public readonly HttpClient httpClient;

        public WalletInterfaceIMPL(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            this.httpClient = httpClient;
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
                PrivateKey = privateKeyHex
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
                PrivateKey = privateKeyWif
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
            var privateKey = account.PrivateKey;
            // Create a new wallet entity
            var walletEntity = new Model.Wallet
            {
                UserID = id, // Replace with actual user ID retrieval logic
                PairName = "ETH",
                Adress = account.Address,
                Amount = 0,
                SeedPhrase = seedPhrase,
                CurrentPrice = 0,
                PrivateKey = privateKey
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

        public async Task CheckAndCreateWallets(string userID)
        {
            var pairs = await _context.Pair
                  .Select(p => new { p.PaiName, p.NetworkName })
                  .ToListAsync();

            foreach (var pair in pairs)
            {
                if (pair.NetworkName == "BTC")
                {
                   await CreateBTCWallet(userID);
                }
                if (pair.NetworkName == "ETH")
                {
                    await CreateETHWallet(userID);
                }
                if (pair.NetworkName == "BNB")
                {
                    await CreateBNBWallet(userID);
                }
            }
        }
        private class BlockCypherBalanceResponse
        {
            public long balance { get; set; }
        }
        private class CheckAndCreateDTO
        {
            public string PaiName { get; set; }
            public string networkName { get; set; }
        }
    }
}
