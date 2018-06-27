using System;
using DbUpgrader.DatabaseManagers;
using DbUpgrader.Definition;
using DbUpgrader.Generators;
using DbUpgrader.MySql;
using MySql.Data.MySqlClient;

namespace DbUpgrader
{
    public class MySqlManager : AnsiDatabaseManager
    {
        private readonly MySqlGenerator _generator = new MySqlGenerator();

        public MySqlManager(string connectionString)
            : base(connectionString, MySqlClientFactory.Instance)
        {
        }

        public override void SetDatabaseName(string databaseName)
        {
            var builder = new MySqlConnectionStringBuilder(this.ConnectionString)
            {
                Database = databaseName
            };
            this.ConnectionString = builder.ConnectionString;
        }

        public override void CreateField(ITable table, IField field)
        {
            var sql = SqlGenerator.GenerateCreateFieldStatement(_generator, table, field);
            ExecuteNonQuery(sql);
        }

        public override bool DatabaseExists(string databaseName)
        {
            var sql = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @dbname";
            return ExecuteScalar(sql, new MySqlParameter("@dbname", databaseName)) != null;
        }

        public override void CreateTable(ITable table)
        {
            var sql = SqlGenerator.GenerateCreateTableStatement(_generator, table);
            ExecuteNonQuery(sql);
        }

        public override FieldType GetFieldTypeFromSourceType(string sourceType)
        {
            if (sourceType.Equals("varchar", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.String;
            }
            throw new Exception("Field type " + sourceType + " is not supported.");
        }

        public override void AlterField(ITable table, IField field)
        {
            var sql = SqlGenerator.GenerateAlterTableAndFieldStatement(_generator, table, field, "MODIFY COLUMN");
            ExecuteNonQuery(sql);
        }
    }
}