using ArsaRExCH.Model;
using Microsoft.Data.SqlClient;

namespace ArsaRExCH.StaticsHelper
{
    public static class StaticMethods
    {
        /*If you dnt want use efcore i made method to insert data by ado.net raw sql commands*
         * Post Command :  to save data into data base in form of Bet Class .
         * Accepts bet class and conectionstring .
         */
        public static async Task InsertBetAsync(Bet bet, string connectionString)
        {
            string query = @"
    UPDATE Bets
    SET 
        UserIdSec = @UserIdSec,
        BtcPriceNow = @BtcPriceNow,
        BtcPriceExpireBet = @BtcPriceExpireBet,
        HitDateBTC = @HitDateBTC,
        BetAmountBtc = @BetAmountBtc,
        EthPriceNow = @EthPriceNow,
        HitDateETH = @HitDateETH,
        ETHPriceNow = @ETHPriceNow,
        EthPriceExpireBet = @EthPriceExpireBet,
        BetAmountETH = @BetAmountETH,
        WiningAmount = @WiningAmount,
        IsBetActive = @IsBetActive,
        ISDeleted = @ISDeleted,
        CompletedTime = @CompletedTime
    WHERE 
        BetId = @BetId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@BetId", bet.BetId);
                    command.Parameters.AddWithValue("@UserIdSec", bet.UserIdSec);
                    command.Parameters.AddWithValue("@BtcPriceNow", bet.BtcPriceNow);
                    command.Parameters.AddWithValue("@BtcPriceExpireBet", bet.BtcPriceExpireBet);
                    command.Parameters.AddWithValue("@HitDateBTC", bet.HitDateBTC);
                    command.Parameters.AddWithValue("@BetAmountBtc", bet.BetAmountBtc);
                    command.Parameters.AddWithValue("@EthPriceNow", bet.EthPriceNow);
                    command.Parameters.AddWithValue("@HitDateETH", bet.HitDateETH);                    command.Parameters.AddWithValue("@EthPriceExpireBet", bet.EthPriceExpireBet);
                    command.Parameters.AddWithValue("@BetAmountETH", bet.BetAmountETH);
                    command.Parameters.AddWithValue("@WiningAmount", bet.WiningAmount.HasValue ? (object)bet.WiningAmount.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@IsBetActive", bet.IsBetActive);
                    command.Parameters.AddWithValue("@ISDeleted", bet.ISDeleted);
                    command.Parameters.AddWithValue("@CompletedTime", bet.CompletedTime);

                    // Open the connection to the database
                    await connection.OpenAsync();

                    // Execute the update command
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new Exception($"No bet found with BetId {bet.BetId}");
                    }
                }
            }
        }

        /* We need to make static method or any other type of method to calculate rewarding system*/
        public static async Task<Bet> CalculateBetResault(Bet bet)
        {
            // First of first we calculate each bet resault  on 00:00  every day 
            //we get the bet and check the Guesses pair price in this case 
            // BTC if the price matches the current price (+ - 200 $) or any other amount the placed bet wi be x 7
            // in any other condition its lost position . 
            //User can not place bet for the same day . We set the available dates for bet .(Phase place bet )
            //We check in this case Binance publlic API to get price AT 00;00 
            //This sample method will return Final balance or final amount of user (intern)
            //about the current rewarding system maybe new thing will be come but not now , to make it more flixable to win
            return null;
        }
    }
}
