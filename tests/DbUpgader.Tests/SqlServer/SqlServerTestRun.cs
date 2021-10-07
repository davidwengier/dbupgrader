using System;
using System.Data.SqlClient;

namespace DbUpgrader.Tests.SqlServer
{
    internal class SqlServerTestRun : IDisposable
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public SqlServerTestRun(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public void Dispose()
        {
            using var conn = new SqlConnection(_connectionString);
            using var comm = new SqlCommand();
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = "ALTER DATABASE [" + _databaseName + "] SET single_user WITH ROLLBACK IMMEDIATE";
            comm.ExecuteNonQuery();
            comm.CommandText = "DROP DATABASE [" + _databaseName + "]";
            comm.ExecuteNonQuery();
        }
    }
}
