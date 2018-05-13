using System;
using System.Data.SqlClient;

namespace DbUpgrader.Tests.Integration
{
    internal class SqlAssert
    {
        internal static void TableExists(string connectionString, string databaseName, string tableName)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var comm = new SqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "SELECT COUNT(*) FROM [" + databaseName + "].INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
                comm.Parameters.Add(new SqlParameter("tableName", tableName));
                if (Convert.ToInt32(comm.ExecuteScalar()) <= 0)
                {
                    throw new Exception("Table '" + tableName + "' does not exist.");
                }
            }
        }
    }
}