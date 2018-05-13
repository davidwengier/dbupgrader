using System;
using System.Data.Common;
using DbUpgrader.Definition;

namespace DbUpgrader.DatabaseManagers
{
    /// <summary>
    /// Provides helpers for database engines with System.Data.Common providers
    /// </summary>
    public abstract class CommonDatabaseManager : IDestinationManager
    {
        private DbProviderFactory _factory;

        protected DbProviderFactory Factory => _factory;

        protected string ConnectionString { get; set; }

        protected CommonDatabaseManager(string connectionString, DbProviderFactory factory)
        {
            this.ConnectionString = connectionString;
            _factory = factory;
        }

        protected DbParameter CreateParameter(string name, object value)
        {
            DbParameter parameter = this.Factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;        }

        protected int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            using (DbConnection conn = OpenConnection())
            using (DbCommand comm = InitializeCommand(conn, sql, parameters))
            {
                return comm.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string sql, params DbParameter[] parameters)
        {
            using (DbConnection conn = OpenConnection())
            using (DbCommand comm = InitializeCommand(conn, sql, parameters))
            {
                return comm.ExecuteScalar();
            }
        }

        private DbCommand InitializeCommand(DbConnection conn, string sql, DbParameter[] parameters)
        {
            var comm = _factory.CreateCommand();
            comm.Connection = conn;
            comm.CommandText = sql;
            comm.CommandType = System.Data.CommandType.Text;
            foreach (DbParameter parameter in parameters)
            {
                comm.Parameters.Add(parameter);
            }
            return comm;
        }

        public bool TryDbConnect()
        {
            try
            {
                using (DbConnection conn = OpenConnection())
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
    }
}