using System;
using System.Linq;
using Xunit;

namespace DbUpgrader.Tests.Integration
{
    public class SqlServerTests
    {
        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            string connectionString = @"Server=(localdb)\v11.0;Integrated Security=true;";
            Upgrader upgrader = DbUpgrader.Upgrade
                                          .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                          .ToSqlServer(connectionString)
                                          .Build();

            Assert.True(upgrader.Run());

            SqlAssert.TableExists(connectionString, "MyTable");
        }
    }
}