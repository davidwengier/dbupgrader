using System;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration
{
    public class SqlServerTests : IDisposable
    {
        private readonly ITestOutputHelper _output;



        public SqlServerTests(ITestOutputHelper output)
        {
            _output = output;
        }

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
                                     .LogToXunit(_output)
                                     .Build();

            Assert.True(upgrader.Run());

            SqlAssert.TableExists(connectionString, "MyDatabase", "MyTable");
        }
    }
}