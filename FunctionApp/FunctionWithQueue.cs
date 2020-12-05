using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class FunctionWithQueue
    {
        [FunctionName("FunctionWithQueue")]
        public static void Run([QueueTrigger("function-update-date-queue", Connection = "AzureWebJobsStorage")] Heroi heroi, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed.");

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Heroi] SET [UltimaVisualizacao] = GETDATE() WHERE [Id] = {heroi.Id};";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }
        }
    }

    public class Heroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}