using System;
using System.Data.SqlClient;

namespace DbUpgrader.Tests.Integration
{
    internal class SqlAssert
    {
        internal static void TableExists(string connectionString, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand comm = new SqlCommand())
            {
                comm.Connection = conn;
                comm.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
                comm.Parameters.Add(new SqlParameter("tableName", tableName));
                if (Convert.ToInt32(comm.ExecuteScalar()) <= 0)
                {
                    throw new Exception("Table '" + tableName + "' does not exist.");
                }
            }
        }
    }
}