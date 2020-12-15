using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLogger
{
    public class Logger
    {
        private readonly string connectionString;

        public Logger(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void LogError(string message)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();

                using (var sqlCommand = new SqlCommand("ExceptionLogger", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Transaction = transaction;

                    try
                    {
                        var errorMessage = sqlCommand.Parameters.AddWithValue("@ErrorMessage", message);
                        var timeMessage  = sqlCommand.Parameters.AddWithValue("@Time", DateTime.Now.ToString());
                        var execute = sqlCommand.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
