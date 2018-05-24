using System;
using System.Data.SqlClient;

namespace DbUpgrader.Tests.SqlServer
{
    internal class SqlAssert
    {
        internal static void TableExists(string connectionString, string databaseName, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM [" + databaseName + "].INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqlParameter("tableName", tableName))) == 0)
            {
                throw new Exception("Table '" + tableName + "' does not exist.");
            }
        }

        internal static void FieldExists(string connectionString, string databaseName, string tableName, string fieldName)
        {
            var sql = "SELECT COUNT(*) FROM [" + databaseName + "].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqlParameter("tableName", tableName), new SqlParameter("fieldName", fieldName))) == 0)
            {
                throw new Exception("Field '" + fieldName + "' doesn't exist in '" + tableName + "' does not exist.");
            }
        }
        internal static void FieldSizeEquals(int size, string connectionString, string databaseName, string tableName, string fieldName)
        {
            var sql = "SELECT CHARACTER_MAXIMUM_LENGTH FROM [" + databaseName + "].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            var actual = Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqlParameter("tableName", tableName), new SqlParameter("fieldName", fieldName)));
            if (size != actual)
            {
                throw new Exception("Field '" + fieldName + "' in table '" + tableName + "' is not " + size + " characters long, its " + actual);
            }
        }

        private static object ExecuteScalar(string connectionString, string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var comm = new SqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.Parameters.AddRange(parameters);
                return comm.ExecuteScalar();
            }
        }

    
    }
}