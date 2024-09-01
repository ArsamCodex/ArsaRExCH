﻿using ArsaRExCH.Data;
using ArsaRExCH.DTOs;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Contracts.Standards.ENS.ETHRegistrarController.ContractDefinition;
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
        public Task SendEmailAsync(EmailRequest emailreqquest);
        Task<double> GetBTCBalanceOfWallet(string walletId);
        //TODO
        //make 2 methode to check balance of bnb and ether
        public Task<List<WalletDTO>> GetWalletsListed(string id);
        //I use this method to get everyuser Balance of inter 
        Task<(List<double> BtcBalances, List<double> EthBalances, List<double> BnbBalances)> GetWalletBalancesForUser(string userId);

    }
}
