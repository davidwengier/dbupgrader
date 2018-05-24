using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.MySql
{
    internal class MySqlHelper : IDestinationManagerHelper
    {
        private string _connectionString;

        public MySqlHelper()
        {
        }

        public MySqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UpgraderBuilder Init(UpgraderBuilder builder)
        {
            return builder.ToMySql(_connectionString);
        }

        public IDisposable TestRun()
        {
            return new MySqlTestRun(_connectionString, "MyDatabase");
        }

        public void AssertFieldExists(string databaseName, string tableName, string fieldName)
        {
            Assert.FieldExists(_connectionString, databaseName, tableName, fieldName);
        }

        public void AssertFieldSizeEquals(int size, string databaseName, string tableName, string fieldName)
        {
            Assert.FieldSizeEquals(size, _connectionString, databaseName, tableName, fieldName);
        }

        public void AssertTableExists(string databaseName, string tableName)
        {
            Assert.TableExists(_connectionString, databaseName, tableName);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(_connectionString), _connectionString);
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            _connectionString = info.GetValue<string>(nameof(_connectionString));
        }

        public override string ToString()
        {
            return "MySql";
        }
    }
}