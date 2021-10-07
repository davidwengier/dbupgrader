using System;
using DbUpgrader.Definition;
using Microsoft.Data.Sqlite;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Sqlite
{
    internal class SqliteHelper : IDestinationManagerTestHelper
    {
        private string _fileName;
        private string ConnectionString => "Data Source=" + _fileName;

        public SqliteHelper()
        {
        }

        public SqliteHelper(string fileName)
        {
            _fileName = fileName;
        }

        public bool ShouldRun()
        {
            try
            {
                using var conn = new SqliteConnection(this.ConnectionString);
                conn.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public UpgraderBuilder Init(UpgraderBuilder builder)
        {
            return builder.ToSqlite(this.ConnectionString);
        }

        public IDisposable TestRun()
        {
            return new SqliteTestRun(_fileName);
        }

        public void AssertFieldExists(string databaseName, string tableName, string fieldName)
        {
            Assert.FieldExists(this.ConnectionString, tableName, fieldName);
        }

        public void AssertFieldSizeEquals(string databaseName, string tableName, string fieldName, int size)
        {
            Assert.FieldSizeEquals(size, this.ConnectionString, tableName, fieldName);
        }

        public void AssertFieldTypeEquals(string databaseName, string tableName, string fieldName, FieldType type)
        {
            Assert.FieldTypeEquals(type, this.ConnectionString, tableName, fieldName);
        }

        public void AssertTableExists(string databaseName, string tableName)
        {
            Assert.TableExists(this.ConnectionString, tableName);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(_fileName), _fileName);
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            _fileName = info.GetValue<string>(nameof(_fileName));
        }

        public override string ToString()
        {
            return "Sqlite";
        }
    }
}
