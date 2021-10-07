using System.Data.Common;
using DbUpgrader.Definition;

namespace DbUpgrader.DatabaseManagers
{
    /// <summary>
    /// Provides helpers for database engines with System.Data.Common providers
    /// </summary>
    public abstract class CommonDatabaseManager : IDestinationManager
    {
        private readonly DbProviderFactory _factory;

        protected DbProviderFactory Factory => _factory;

        protected string ConnectionString { get; set; }

        protected CommonDatabaseManager(string connectionString, DbProviderFactory factory)
        {
            this.ConnectionString = connectionString;
            _factory = factory;
        }

        protected DbParameter CreateParameter(string name, object value)
        {
            var parameter = this.Factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        protected int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            using (var conn = OpenConnection())
            using (var comm = InitializeCommand(conn, sql, parameters))
            {
                return comm.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string sql, params DbParameter[] parameters)
        {
            using (var conn = OpenConnection())
            using (var comm = InitializeCommand(conn, sql, parameters))
            {
                return comm.ExecuteScalar();
            }
        }

        protected DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
        {
            var conn = OpenConnection();
            var comm = InitializeCommand(conn, sql, parameters);
            return comm.ExecuteReader();
        }

        private DbCommand InitializeCommand(DbConnection conn, string sql, DbParameter[] parameters)
        {
            var comm = _factory.CreateCommand();
            comm.Connection = conn;
            comm.CommandText = sql;
            comm.CommandType = System.Data.CommandType.Text;
            foreach (var parameter in parameters)
            {
                comm.Parameters.Add(parameter);
            }
            return comm;
        }

        public bool TryDbConnect()
        {
            try
            {
                using (var conn = OpenConnection())
                {
                }
                return true;
            }
            catch (DbException)
            {
                return false;
            }
        }

        private DbConnection OpenConnection()
        {
            var conn = _factory.CreateConnection();
            conn.ConnectionString = this.ConnectionString;
            conn.Open();
            return conn;
        }

        public abstract void SetDatabaseName(string databaseName);

        public abstract bool DatabaseExists(string databaseName);

        public abstract void CreateDatabase(string databaseName);

        public abstract bool TableExists(ITable table);

        public abstract void CreateTable(ITable table);

        public abstract bool FieldExists(ITable table, IField field);

        public abstract void CreateField(ITable table, IField field);

        public abstract IField GetFieldInfo(string tableName, string fieldName);

        public abstract void AlterField(ITable table, IField field);

        public abstract FieldType GetFieldTypeFromSourceType(string sourceType);
    }
}
