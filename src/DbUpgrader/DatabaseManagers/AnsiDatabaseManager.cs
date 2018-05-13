using System;
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
            string sql = "CREATE DATABASE [" + databaseName + "]";
            ExecuteNonQuery(sql);
        }

        public override bool TableExists(ITable table)
        {
            const string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }
    }
}