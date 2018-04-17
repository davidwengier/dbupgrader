using System;
using System.Data.Common;
using DbUpgrader.Definition;

namespace DbUpgrader.AnsiDatabase
{
    public abstract class AnsiDatabaseManager : IDestinationManager
    {
        private string _connectionString;

        protected string ConnectionString => _connectionString;

        protected AnsiDatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected abstract DbConnection CreateConnection();

        protected abstract DbCommand CreateCommand();

        protected abstract DbParameter CreateParameter(string name, object value);

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
            var comm = CreateCommand();
            comm.Connection = conn;
            comm.CommandText = sql;
            comm.CommandType = System.Data.CommandType.Text;
            comm.Parameters.AddRange(parameters);
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
            var conn = CreateConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            return conn;
        }

        public abstract bool DatabaseExists(string databaseName);

        public virtual void CreateDatabase(string databaseName)
        {
            string sql = "CREATE DATABASE [" + databaseName + "]";
            ExecuteNonQuery(sql);
        }

        public virtual bool TableExists(ITable table)
        {
            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            return ExecuteScalar(sql, CreateParameter("@tableName", table.Name)) != null;
        }

        public virtual void CreateTable(ITable table)
        {
            string sql = "CREATE TABLE [" + table.Name + "]";
            ExecuteNonQuery(sql);
        }

        public abstract bool FieldExists(ITable table, IField field);

        public abstract void CreateField(ITable table, IField field);
    }
}