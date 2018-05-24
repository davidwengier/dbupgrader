using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Sqlite
{
    internal class SqliteHelper : IDestinationManagerHelper
    {
        private string _fileName;

        public SqliteHelper()
        {
        }

        public SqliteHelper(string fileName)
        {
            _fileName = fileName;
        }

        public UpgraderBuilder Init(UpgraderBuilder builder)
        {
            return builder.ToSqlite(_fileName);
        }

        public IDisposable TestRun()
        {
            return new SqliteTestRun(_fileName);
        }

        public void AssertFieldExists(string databaseName, string tableName, string fieldName)
        {
            Assert.FieldExists(_fileName, databaseName, tableName, fieldName);
        }

        public void AssertFieldSizeEquals(int size, string databaseName, string tableName, string fieldName)
        {
            Assert.FieldSizeEquals(size, _fileName, databaseName, tableName, fieldName);
        }

        public void AssertTableExists(string databaseName, string tableName)
        {
            Assert.TableExists(_fileName, databaseName, tableName);
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