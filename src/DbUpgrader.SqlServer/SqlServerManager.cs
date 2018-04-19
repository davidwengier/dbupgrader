using System.Data.Common;
using System.Data.SqlClient;
using DbUpgrader.DatabaseManagers;
using DbUpgrader.Definition;
using DbUpgrader.Generators;

namespace DbUpgrader
{
    internal class SqlServerManager : AnsiDatabaseManager, ISqlGenerator
    {
        public SqlServerManager(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbCommand CreateCommand() => new SqlCommand();

        protected override DbConnection CreateConnection() => new SqlConnection();

        protected override DbParameter CreateParameter(string name, object value) => new SqlParameter(name, value);

        public override void CreateField(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }

        public override bool DatabaseExists(string databaseName)
        {
            string sql = "SELECT name FROM master.dbo.sysdatabases WHERE('[' + name + ']' = @dbname OR name = @dbname)";
            return ExecuteScalar(sql, new SqlParameter("@dbname", databaseName)) != null;
        }

        public override bool FieldExists(ITable table, IField field)
        {
            throw new System.NotImplementedException();
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
                case FieldType.String when field.Size > 0:
                {
                    return "VARCHAR(MAX)";
                }
                case FieldType.String:
                {
                    return "VARCHAR(" + field.Size + ")";
                }
            }
			return null;
		}
	}
}