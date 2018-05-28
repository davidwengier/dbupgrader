using System;
using DbUpgrader.Definition;
using MySql.Data.MySqlClient;

namespace DbUpgrader.Tests.MySql
{
    internal class Assert
    {
        internal static void TableExists(string connectionString, string databaseName, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @databaseName AND TABLE_NAME = @tableName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new MySqlParameter("databaseName", databaseName), new MySqlParameter("tableName", tableName))) == 0)
            {
                throw new Exception("Table '" + tableName + "' does not exist.");
            }
        }

        internal static void FieldExists(string connectionString, string databaseName, string tableName, string fieldName)
        {
            var sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @databaseName AND TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new MySqlParameter("databaseName", databaseName), new MySqlParameter("tableName", tableName), new MySqlParameter("fieldName", fieldName))) == 0)
            {
                throw new Exception("Field '" + fieldName + "' doesn't exist in '" + tableName + "' does not exist.");
            }
        }

        internal static void FieldSizeEquals(int size, string connectionString, string databaseName, string tableName, string fieldName)
        {
            var sql = "SELECT CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @databaseName AND TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            var actual = Convert.ToInt32(ExecuteScalar(connectionString, sql, new MySqlParameter("databaseName", databaseName), new MySqlParameter("tableName", tableName), new MySqlParameter("fieldName", fieldName)));
            if (size != actual)
            {
                throw new Exception("Field '" + fieldName + "' in table '" + tableName + "' is not " + size + " characters long, its " + actual);
            }
        }

        internal static void FieldTypeEquals(FieldType type, string connectionString, string databaseName, string tableName, string fieldName)
        {
            var sql = "SELECT DATA_TYPE FROM [" + databaseName + "].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            var actual = ExecuteScalar(connectionString, sql, new MySqlParameter("tableName", tableName), new MySqlParameter("fieldName", fieldName)).ToString();
            if (type != GetFieldTypeForDataType(actual))
            {
                throw new Exception("Field '" + fieldName + "' in table '" + tableName + "' is not a " + type + ", its " + actual);
            }
        }

        private static FieldType GetFieldTypeForDataType(string actual)
        {
            if (actual.Equals("varchar", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.String;
            }
            return (FieldType)(-1);
        }

        private static object ExecuteScalar(string connectionString, string sql, params MySqlParameter[] parameters)
        {
            using (var conn = new MySqlConnection(connectionString))
            using (var comm = new MySqlCommand())
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