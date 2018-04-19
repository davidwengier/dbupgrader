using System;
using System.Data.Common;
using DbUpgrader.Definition;

namespace DbUpgrader.DatabaseManagers
{
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

		public override void CreateTable(ITable table)
		{
			string sql = "CREATE TABLE [" + table.Name + "]";
			ExecuteNonQuery(sql);
		}
	}
}