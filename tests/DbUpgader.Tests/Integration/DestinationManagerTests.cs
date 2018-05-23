using System;
using System.Collections.Generic;
using System.Linq;
using DbUpgrader.Definition;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Integration
{
    public class DestinationManagerTests
    {
        public delegate UpgraderBuilder InitFunc(UpgraderBuilder builder);

        private readonly ITestOutputHelper _output;

        public DestinationManagerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [MemberData(nameof(GetTestHelpers))]
        public void NoDatabase_Create_TableAndFieldExists(IDestinationManagerHelper db)
        {
            using (db.TestRun())
            {
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                     .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                     .LogToXunit(_output)
                                     .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals(20, "MyDatabase", "MyTable", "MyField");
            }
        }

        [Theory]
        [MemberData(nameof(GetTestHelpers))]
        public void ExistingDatabase_FieldSizeChanged_Changed(IDestinationManagerHelper db)
        {
            using (db.TestRun())
            {
                var definition = TestData.CreateSimpleDatabaseDefinition();
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                         .FromDefinition(definition)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals(20, "MyDatabase", "MyTable", "MyField");

                ((Field)definition.GetTables().First().GetFields().First()).Size = 50;

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals(50, "MyDatabase", "MyTable", "MyField");
            }
        }

        [Theory]
        [MemberData(nameof(GetTestHelpers))]
        public void ExistingDatabase_FieldAdded_NewFieldExists(IDestinationManagerHelper db)
        {
            using (db.TestRun())
            {
                var definition = TestData.CreateSimpleDatabaseDefinition();
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                         .FromDefinition(definition)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals(20, "MyDatabase", "MyTable", "MyField");

                ((Table)definition.GetTables().First()).AddField(new Field("Field2", FieldType.String, 10));

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldExists("MyDatabase", "MyTable", "Field2");
            }
        }

        public static IEnumerable<object[]> GetTestHelpers()
        {
            yield return new object[] { new SqlServer.SqlServerHelper(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;") };
            yield return new object[] { new InMemory.InMemoryHelper() };
        }
    }
}