using System;
using Xunit;

namespace DbUpgrader.Tests.Integration
{
    public class SqlServerTests : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;";
            var upgrader = DbUpgrader.Upgrade
                                     .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                     .ToSqlServer(connectionString)
                                     .LogToConsole()
                                     .Build();

            Assert.True(upgrader.Run());

            SqlAssert.TableExists(connectionString, "MyDatabase", "MyTable");
        }
    }
}