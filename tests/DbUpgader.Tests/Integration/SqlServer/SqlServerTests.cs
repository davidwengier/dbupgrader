using System;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration
{
    public class SqlServerTests : IDisposable
    {
        private ITestOutputHelper _output;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public SqlServerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;";
            var upgrader = DbUpgrader.Upgrade
                                     .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                     .ToSqlServer(connectionString)
                                     .LogToXunit(_output)
                                     .Build();

            Assert.True(upgrader.Run());

            SqlAssert.TableExists(connectionString, "MyDatabase", "MyTable");
        }
    }
}