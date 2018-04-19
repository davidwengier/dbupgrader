using System.Data.Common;
using DbUpgrader.DatabaseManagers;
using DbUpgrader.Definition;
using DbUpgrader.Generators;
using Microsoft.Data.Sqlite;

namespace DbUpgrader
{
    internal class SqliteManager : CommonDatabaseManager, ISqlGenerator
    {
        public SqliteManager(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbCommand CreateCommand() => new SqliteCommand();

        protected override DbConnection CreateConnection() => new SqliteConnection();

        protected override DbParameter CreateParameter(string name, object value) => new SqliteParameter(name, value);

        public override void CreateField(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }

        public override bool DatabaseExists(string databaseName) => true;

        public override bool FieldExists(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateDatabase(string databaseName)
        {
            throw new System.NotSupportedException();
        }

        public override bool TableExists(ITable table)
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }

        public override void CreateTable(ITable table)
        {
            string sql = SqlGenerator.GenerateCreateTableStatement(this, table);
            ExecuteNonQuery(sql);
        }

        string ISqlGenerator.GetFieldDataType(IField field)
        {
            switch (field.Type)
            {
                case FieldType.String:
                {
                    return "TEXT";
                }
            }
            return null;
        }
    }
}