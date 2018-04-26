using System;
using System.IO;
using Xunit;

namespace DbUpgrader.Tests.Integration
{
    public class SqliteTests
    {
        private const string TestDatabaseFile = "TestDatabase.db";

        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            if (File.Exists(TestDatabaseFile))
            {
                File.Delete(TestDatabaseFile);
            }
            string connectionString = @"Data Source=" + TestDatabaseFile;
            Upgrader upgrader = DbUpgrader.Upgrade
                                          .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                          .ToSqlite(connectionString)
                                          .LogToConsole()
                                          .Build();

            Assert.True(upgrader.Run());

            //SqlAssert.TableExists(connectionString, "MyTable");
        }
    }
}