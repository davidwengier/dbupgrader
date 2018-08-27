using System;
using System.Data.SqlClient;
using DbUpgrader.DatabaseManagers;
using DbUpgrader.Definition;
using DbUpgrader.Generators;
using DbUpgrader.SqlServer;

namespace DbUpgrader
{
    public class SqlServerManager : AnsiDatabaseManager
    {
        private readonly SqlServerSqlGenerator _generator = new SqlServerSqlGenerator();

        public SqlServerManager(string connectionString)
            : base(connectionString, SqlClientFactory.Instance)
        {
        }

        public override void SetDatabaseName(string databaseName)
        {
            var builder = new SqlConnectionStringBuilder(this.ConnectionString)
            {
                InitialCatalog = databaseName
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
            var sql = "SELECT name FROM master.dbo.sysdatabases WHERE('[' + name + ']' = @dbname OR name = @dbname)";
            return ExecuteScalar(sql, new SqlParameter("@dbname", databaseName)) != null;
        }

        public override void CreateTable(ITable table)
        {
            var sql = SqlGenerator.GenerateCreateTableStatement(_generator, table);
            ExecuteNonQuery(sql);
        }

        public override FieldType GetFieldTypeFromSourceType(string sourceType)
        {
            return GetFieldType(sourceType);
        }

        public static FieldType GetFieldType(string sourceType)
        {
            if (sourceType.Equals("varchar", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.String;
            }
            else if (sourceType.Equals("text", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.LongString;
            }
            else if (sourceType.Equals("int", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.Integer;
            }
            else if (sourceType.Equals("decimal", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.Decimal;
            }
            else if (sourceType.Equals("bit", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.Boolean;
            }
            else if (sourceType.Equals("varbinary", StringComparison.OrdinalIgnoreCase))
            {
                return FieldType.Binary;
            }
            throw new Exception("Field type " + sourceType + " is not supported.");
        }

        public override void AlterField(ITable table, IField field)
        {
            var sql = SqlGenerator.GenerateAlterFieldStatement(_generator, table, field);
            ExecuteNonQuery(sql);
        }
    }
}