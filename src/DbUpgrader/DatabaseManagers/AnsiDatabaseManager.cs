using System;
using System.Data.Common;
using System.Text;
using DbUpgrader.Definition;

namespace DbUpgrader.DatabaseManagers
{
    /// <summary>
    /// Provides basic functionality for database engines that support the ANSI standard for INFORMATION_SCHEMA
    /// </summary>
    public abstract class AnsiDatabaseManager : CommonDatabaseManager
    {
        protected AnsiDatabaseManager(string connectionString)
            : base(connectionString)
        {
        }

        public override void CreateDatabase(string databaseName)
        {
            string sql = "CREATE DATABASE [" + databaseName + "]";
            ExecuteNonQuery(sql);
        }

        public override bool TableExists(ITable table)
        {
            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }

    }
}