using System;
using MySql.Data.MySqlClient;

namespace DbUpgrader.Tests.MySql
{
    internal class MySqlTestRun : IDisposable
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public MySqlTestRun(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public void Dispose()
        {
            using var conn = new MySqlConnection(_connectionString);
            using var comm = new MySqlCommand();
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = "DROP DATABASE " + _databaseName;
            comm.ExecuteNonQuery();
        }
    }
}
