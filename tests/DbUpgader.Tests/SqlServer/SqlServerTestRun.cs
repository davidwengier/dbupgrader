using System;
using System.Data.SqlClient;
using System.Linq;

namespace DbUpgrader.Tests.SqlServer
{
    internal class SqlServerTestRun : IDisposable
    {
        private string _connectionString;
        private string _databaseName;

        public SqlServerTestRun(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

#pragma warning disable CA1063 // Implement IDisposable Correctly

        public void Dispose()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var comm = new SqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "ALTER DATABASE [" + _databaseName + "] SET single_user WITH ROLLBACK IMMEDIATE";
                comm.ExecuteNonQuery();
                comm.CommandText = "DROP DATABASE [" + _databaseName + "]";
                comm.ExecuteNonQuery();
            }
        }

#pragma warning restore CA1063 // Implement IDisposable Correctly
    }
}