using System.Data.Common;
using System.Data.SqlClient;
using DbUpgrader.AnsiDatabase;
using DbUpgrader.Definition;

namespace DbUpgrader
{
    internal class SqlServerManager : AnsiDatabaseManager
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
    }
}