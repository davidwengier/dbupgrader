using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUpgrader.Tests.Integration.SqlServer
{
    internal class TempSqlDatabase : IDisposable
    {
        private string _connectionString;
        private string _databaseName;

        public TempSqlDatabase(string connectionString, string v)
        {
            _connectionString = connectionString;
            _databaseName = v;
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
