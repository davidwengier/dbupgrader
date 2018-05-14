using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration
{
    public class SqliteTests
    {
        private readonly ITestOutputHelper _output;

        private const string TestDatabaseFile = "TestDatabase.db";

        public SqliteTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            if (File.Exists(TestDatabaseFile))
            {
                File.Delete(TestDatabaseFile);
            }
            var connectionString = @"Data Source=" + TestDatabaseFile;
            var upgrader = DbUpgrader.Upgrade
                                     .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                     .ToSqlite(connectionString)
                                     .LogToXunit(_output)
                                     .Build();

            Assert.True(upgrader.Run());

            //SqlAssert.TableExists(connectionString, "MyTable");
        }
    }
}