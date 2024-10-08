namespace ArsaRExCH.Interface
{
    public interface IBiTiCi
    {
        /* To implement this method i need Bitcoin Nodeto on my machine make transactions at the time i dont have 
         * any pc to run Node , instead i can impelement client for all exchanges like Binance okex bybit to make transactions from there 
         * for get this done we need to make client and api calls , 
         */
        Task<string> WithdrawBtcAsync(string privateKey, string recipientAddress, decimal amount);

    }
}
