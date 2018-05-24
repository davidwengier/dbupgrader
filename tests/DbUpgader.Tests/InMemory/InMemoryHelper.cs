using System;
using System.Linq;
using DbUpgrader.Definition;
using DbUpgrader.Tests.InMemory;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.InMemory
{
    internal class InMemoryHelper : IDestinationManagerHelper
    {
        private InMemoryDatabase _database = new InMemoryDatabase();

        public InMemoryHelper()
        {
        }

        public UpgraderBuilder Init(UpgraderBuilder upgrader)
        {
            return upgrader.To(_database);
        }

        public IDisposable TestRun()
        {
            return null;
        }

        public void AssertFieldExists(string databaseName, string tableName, string fieldName)
        {
            var table = GetTable(tableName);
            Assert.NotNull(table);
            var field = GetField(table, fieldName);
            Assert.NotNull(field);
        }

        public void AssertFieldSizeEquals(int size, string databaseName, string tableName, string fieldName)
        {
            var table = GetTable(tableName);
            var field = GetField(table, fieldName);
            Assert.Equal(size, field.Size);
        }

        public void AssertTableExists(string databaseName, string tableName)
        {
            var table = GetTable(tableName);
            Assert.NotNull(table);
        }

        private static IField GetField(Table table, string fieldName)
        {
            return table.GetFields().FirstOrDefault(f => f.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        private Definition.Table GetTable(string tableName)
        {
            return _database.Tables.FirstOrDefault(t => t.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
        }

        public override string ToString()
        {
            return "In Memory Database";
        }
    }
}