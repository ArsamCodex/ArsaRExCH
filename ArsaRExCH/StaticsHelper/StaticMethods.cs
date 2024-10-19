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
        /*
        public static async Task<string> GetClientIpAddress(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = httpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0].Trim();
            }
            else if (ipAddress != null && ipAddress.Contains(":"))
            {
                // Convert IPv6 address to IPv4 if necessary
                ipAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            return ipAddress ?? "Unknown"; // Return "Unknown" if IP address cannot be determined
        }
        */

        /*
         * Generic Insert method which you call use for all type classes
         * This method considerd as Insert Or Save data to database 
         * // Example: Insert a new Bet object into the "Bets" table
                Bet newBet = new Bet
                {
                    // Assign values to the properties of the bet
                    BetId = 1,
                    UserIdSec = "User123",
                    BtcPriceNow = 45000,
                    BtcPriceExpireBet = 46000,
                    HitDateBTC = DateTime.Now,
                    BetAmountBtc = 0.1m,
                    EthPriceNow = 3500,
                    HitDateETH = DateTime.Now.AddMinutes(5),
                    EthPriceExpireBet = 3600,
                    BetAmountETH = 0.5m,
                    WiningAmount = null, // Nullable fields can be set to null
                    IsBetActive = true,
                    ISDeleted = false,
                    CompletedTime = null // Nullable fields can be set to null
                };

                await InsertEntityAsync(newBet, "Bets", connectionString);
                         */
        public static async Task InsertEntityAsync<T>(T entity, string tableName, string connectionString) where T : class
        {
            var properties = typeof(T).GetProperties();

            // Get column names and parameter placeholders
            var columnNames = properties.Select(p => p.Name).ToList();
            var parameterNames = properties.Select(p => $"@{p.Name}").ToList();

            // Build the insert query
            string query = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", parameterNames)})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters dynamically
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(entity) ?? DBNull.Value;
                        command.Parameters.AddWithValue($"@{property.Name}", value);
                    }

                    // Open the connection to the database
                    await connection.OpenAsync();

                    // Execute the insert command
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Insert failed for entity of type {typeof(T).Name}");
                    }
                }
            }
        }
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
    }
}
