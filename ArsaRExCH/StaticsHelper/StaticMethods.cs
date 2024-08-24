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
    }
}
