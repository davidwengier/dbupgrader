using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DbUpgrader.Tests.MySql
{
    internal class MySqlTestRun : IDisposable
    {
        private string _connectionString;
        private string _databaseName;

        public MySqlTestRun(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

#pragma warning disable CA1063 // Implement IDisposable Correctly

        public void Dispose()
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var comm = new MySqlCommand())
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "DROP DATABASE " + _databaseName;
                comm.ExecuteNonQuery();
            }
        }

#pragma warning restore CA1063 // Implement IDisposable Correctly
    }
}