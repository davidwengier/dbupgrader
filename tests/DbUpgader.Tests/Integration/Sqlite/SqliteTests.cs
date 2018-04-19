using System;
using System.Linq;
using Xunit;

namespace DbUpgrader.Tests.Integration
{
    public class SqliteTests
    {
        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            string connectionString = @"Data Source=TestDatabase.db";
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