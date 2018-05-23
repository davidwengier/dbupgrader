using System;
using System.Linq;
using DbUpgrader.Definition;
using DbUpgrader.Tests.Integration.SqlServer;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration
{
    public class SqlServerTests
    {
        private readonly ITestOutputHelper _output;

        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;";

        public SqlServerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void NoDatabase_TableAndField_Created()
        {
            using (new TempSqlDatabase(ConnectionString, "MyDatabase"))
            {
                var upgrader = DbUpgrader.Upgrade
                                         .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                         .ToSqlServer(ConnectionString)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                SqlAssert.TableExists(ConnectionString, "MyDatabase", "MyTable");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "MyField");
                SqlAssert.FieldSizeEquals(20, ConnectionString, "MyDatabase", "MyTable", "MyField");
            }
        }

        [Fact]
        public void ExistingDatabase_FieldSizeChanged_Changed()
        {
            using (new TempSqlDatabase(ConnectionString, "MyDatabase"))
            {
                var definition = TestData.CreateSimpleDatabaseDefinition();
                var upgrader = DbUpgrader.Upgrade
                                         .FromDefinition(definition)
                                         .ToSqlServer(ConnectionString)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                SqlAssert.TableExists(ConnectionString, "MyDatabase", "MyTable");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "MyField");
                SqlAssert.FieldSizeEquals(20, ConnectionString, "MyDatabase", "MyTable", "MyField");

                ((Field)definition.GetTables().First().GetFields().First()).Size = 50;

                Assert.True(upgrader.Run());

                SqlAssert.TableExists(ConnectionString, "MyDatabase", "MyTable");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "MyField");
                SqlAssert.FieldSizeEquals(50, ConnectionString, "MyDatabase", "MyTable", "MyField");
            }
        }

        [Fact]
        public void ExistingDatabase_FieldAdded_NewFieldExists()
        {
            using (new TempSqlDatabase(ConnectionString, "MyDatabase"))
            {
                var definition = TestData.CreateSimpleDatabaseDefinition();
                var upgrader = DbUpgrader.Upgrade
                                         .FromDefinition(definition)
                                         .ToSqlServer(ConnectionString)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                SqlAssert.TableExists(ConnectionString, "MyDatabase", "MyTable");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "MyField");
                SqlAssert.FieldSizeEquals(20, ConnectionString, "MyDatabase", "MyTable", "MyField");

                ((Table)definition.GetTables().First()).AddField(new Field("Field2", FieldType.String, 10));

                Assert.True(upgrader.Run());

                SqlAssert.TableExists(ConnectionString, "MyDatabase", "MyTable");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "MyField");
                SqlAssert.FieldExists(ConnectionString, "MyDatabase", "MyTable", "Field2");
            }
        }
    }
}