using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration.SqlServer
{
    internal class SqlServerHelper : IDestinationManagerHelper
    {
        private string _connectionString;

        public SqlServerHelper()
        {
        }

        public SqlServerHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AssertFieldExists(string databaseName, string tableName, string fieldName)
        {
            SqlAssert.FieldExists(_connectionString, databaseName, tableName, fieldName);
        }

        public void AssertFieldSizeEquals(int v1, string databaseName, string tableName, string fieldName)
        {
            SqlAssert.FieldSizeEquals(v1, _connectionString, databaseName, tableName, fieldName);
        }

        public void AssertTableExists(string databaseName, string tableName)
        {
            SqlAssert.TableExists(_connectionString, databaseName, tableName);
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            _connectionString = info.GetValue<string>(nameof(_connectionString));
        }

        public UpgraderBuilder Init(UpgraderBuilder builder)
        {
            return builder.ToSqlServer(_connectionString);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(_connectionString), _connectionString);
        }

        public IDisposable TestRun()
        {
            return new SqlServerTestRun(_connectionString, "MyDatabase");
        }

        public override string ToString()
        {
            return "Sql Server";
        }
    }
}