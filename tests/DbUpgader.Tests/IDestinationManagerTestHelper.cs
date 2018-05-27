using System;
using Xunit.Abstractions;

namespace DbUpgrader.Tests
{
    public interface IDestinationManagerTestHelper : IXunitSerializable
    {
        bool ShouldRun();

        UpgraderBuilder Init(UpgraderBuilder upgrade);

        void AssertTableExists(string databaseName, string tableName);

        void AssertFieldExists(string databaseName, string tableName, string fieldName);

        void AssertFieldSizeEquals(int size, string databaseName, string tableName, string fieldName);

        IDisposable TestRun();
    }
}