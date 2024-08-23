using ArsaRExCH.Model;
using Microsoft.Data.SqlClient;

namespace ArsaRExCH.StaticsHelper
{
    public static class StaticMethods
    {
        /*If you dnt want use efcore i made method to insert data by ado.net raw sql commands*/
        public static async Task InsertBetAsync(Bet bet, string connectionString)
        {
            string query = @"
            INSERT INTO Bets (UserId, BtcPrice, HitDateBTC, EthPrice, HitDateETH, BetAmount, WiningAmount, IsBetActive, ISDeleted, CompletedTime) 
            VALUES (@UserId, @BtcPrice, @HitDateBTC, @EthPrice, @HitDateETH, @BetAmount, @WiningAmount, @IsBetActive, @ISDeleted, @CompletedTime)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@UserId", bet.UserId);
                    command.Parameters.AddWithValue("@BtcPrice", bet.BtcPrice);
                    command.Parameters.AddWithValue("@HitDateBTC", bet.HitDateBTC);
                    command.Parameters.AddWithValue("@EthPrice", bet.EthPrice);
                    command.Parameters.AddWithValue("@HitDateETH", bet.HitDateETH);
                    command.Parameters.AddWithValue("@BetAmount", bet.BetAmount);
                    command.Parameters.AddWithValue("@WiningAmount", bet.WiningAmount.HasValue ? bet.WiningAmount.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsBetActive", bet.IsBetActive);
                    command.Parameters.AddWithValue("@ISDeleted", bet.ISDeleted);
                    command.Parameters.AddWithValue("@CompletedTime", bet.CompletedTime);

                    // Open the connection to the database
                    await connection.OpenAsync();

                    // Execute the insert command
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
