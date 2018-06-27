using System.Data.Common;
using DbUpgrader.DatabaseManagers;
using DbUpgrader.Definition;
using DbUpgrader.Generators;
using Microsoft.Data.Sqlite;
using DbUpgrader.Sqlite;

namespace DbUpgrader
{
    public class SqliteManager : CommonDatabaseManager
    {
        private readonly SqliteSqlGenetator _generator = new SqliteSqlGenetator();

        public SqliteManager(string connectionString)
            : base(connectionString, SqliteFactory.Instance)
        {
        }

        public override void SetDatabaseName(string databaseName)
        {
            // No need, SQLite is only one db per file
        }

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
            var sql = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }

        public override void CreateTable(ITable table)
        {
            var sql = SqlGenerator.GenerateCreateTableStatement(_generator, table);
            ExecuteNonQuery(sql);
        }

        public override FieldType GetFieldTypeFromSourceType(string sourceType)
        {
            throw new System.NotImplementedException();
        }

        public override IField GetFieldInfo(string tableName, string fieldName)
        {
            throw new System.NotImplementedException();
        }

        public override void AlterField(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }
    }
}