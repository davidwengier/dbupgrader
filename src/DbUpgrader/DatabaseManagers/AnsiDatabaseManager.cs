using System.Data.Common;
using DbUpgrader.Definition;

namespace DbUpgrader.DatabaseManagers
{
    /// <summary>
    /// Provides basic functionality for database engines that support the ANSI standard for INFORMATION_SCHEMA
    /// </summary>
    public abstract class AnsiDatabaseManager : CommonDatabaseManager
    {
        protected AnsiDatabaseManager(string connectionString, DbProviderFactory factory)
            : base(connectionString, factory)
        {
        }

        public override void CreateDatabase(string databaseName)
        {
            var sql = "CREATE DATABASE " + databaseName;
            ExecuteNonQuery(sql);
        }

        public override bool FieldExists(ITable table, IField field)
        {
            const string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name),
                                      CreateParameter("@fieldName", field.Name)) != null;
        }

        public override bool TableExists(ITable table)
        {
            const string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }

        public override IField GetFieldInfo(string tableName, string fieldName)
        {
            const string sql = "SELECT DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            using (var reader = ExecuteReader(sql, CreateParameter("@tableName", tableName), CreateParameter("@fieldName", fieldName)))
            {
                if (reader.Read())
                {
                    return new Field(fieldName, GetFieldTypeFromSourceType(reader.GetString(reader.GetOrdinal("DATA_TYPE"))),
                        reader.GetInt32(reader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH")));
                }
            }
            return null;
        }
    }
}
