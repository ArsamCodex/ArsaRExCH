using ArsaRExCH.Model;
using Microsoft.Data.SqlClient;

namespace ArsaRExCH.StaticsHelper
{
    public static class StaticMethods
    {
        public static double CalculateFee(double totalBtcAmount, double feePercentage)
        {
            if (totalBtcAmount < 0)
            {
                throw new ArgumentException("Total BTC amount cannot be negative.");
            }

            if (feePercentage < 0)
            {
                throw new ArgumentException("Fee percentage cannot be negative.");
            }

            // Calculate the fee amount
            return (totalBtcAmount * feePercentage) / 100;
        }
        // This method accepts the BTC price and the current exchange rate to USDT
        public static Task<double> ConvertBtcToUsdtAsync(double btcAmount, double btcPriceInUsdt)
        {
            if (btcAmount < 0)
            {
                throw new ArgumentException("BTC amount must be greater than or equal to zero.");
            }

            // Calculate the equivalent amount in USDT
            double usdtAmount = btcAmount * btcPriceInUsdt;

            // Return the result wrapped in a Task
            return Task.FromResult(usdtAmount);
        }


    }
}
