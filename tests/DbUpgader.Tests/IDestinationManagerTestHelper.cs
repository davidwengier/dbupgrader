using System;
using DbUpgrader.Definition;
using Xunit.Abstractions;

namespace DbUpgrader.Tests
{
    public interface IDestinationManagerTestHelper : IXunitSerializable
    {
        bool ShouldRun();

        UpgraderBuilder Init(UpgraderBuilder upgrade);

        void AssertTableExists(string databaseName, string tableName);

        void AssertFieldExists(string databaseName, string tableName, string fieldName);

        void AssertFieldSizeEquals(string databaseName, string tableName, string fieldName, int size);

        void AssertFieldTypeEquals(string databaseName, string tableName, string fieldName, FieldType fieldType);

        IDisposable TestRun();
    }
}