using System;
using System.Collections.Generic;
using System.Linq;
using DbUpgrader.Definition;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests
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
        [MemberData(nameof(TestSources.GetTestSources), MemberType = typeof(TestSources))]
        public void NoDatabase_Create_TableAndFieldExists(IDestinationManagerTestHelper db)
        {
            using (db.TestRun())
            {
                var definition = new Database("MyDatabase",
                        new Table(
                            "MyTable",
                            new Field("MyField", FieldType.String, 20)
                            )
                        );
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                     .FromDefinition(definition)
                                     .LogToXunit(_output)
                                     .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals("MyDatabase", "MyTable", "MyField", 20);
            }
        }

        [Theory]
        [MemberData(nameof(TestSources.GetTestSources), MemberType = typeof(TestSources))]
        public void ExistingDatabase_FieldSizeChanged_Changed(IDestinationManagerTestHelper db)
        {
            using (db.TestRun())
            {
                var definition = new Database("MyDatabase",
                        new Table(
                            "MyTable",
                            new Field("MyField", FieldType.String, 20)
                            )
                        );
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                         .FromDefinition(definition)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals("MyDatabase", "MyTable", "MyField", 20);

                ((Field)definition.GetTables().First().GetFields().First()).Size = 50;

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals("MyDatabase", "MyTable", "MyField", 50);
            }
        }

        [Theory]
        [MemberData(nameof(TestSources.GetTestSources), MemberType = typeof(TestSources))]
        public void ExistingDatabase_FieldAdded_NewFieldExists(IDestinationManagerTestHelper db)
        {
            using (db.TestRun())
            {
                var definition = new Database("MyDatabase",
                         new Table(
                             "MyTable",
                             new Field("MyField", FieldType.String, 20)
                             )
                         );
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                         .FromDefinition(definition)
                                         .LogToXunit(_output)
                                         .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldSizeEquals("MyDatabase", "MyTable", "MyField", 20);

                ((Table)definition.GetTables().First()).AddField(new Field("Field2", FieldType.String, 10));

                Assert.True(upgrader.Run());

                db.AssertTableExists("MyDatabase", "MyTable");
                db.AssertFieldExists("MyDatabase", "MyTable", "MyField");
                db.AssertFieldExists("MyDatabase", "MyTable", "Field2");
            }
        }

        [Theory]
        [MemberData(nameof(TestSources.GetTestSources), MemberType = typeof(TestSources))]
        public void NoDatabase_Create_OneOfEachType(IDestinationManagerTestHelper db)
        {
            using (db.TestRun())
            {
                var definition = new Database("OneOfEachDatabase",
                        new Table(
                            "OneOfEach",
                            new Field("StringField", FieldType.String, 20),
                            new Field("IntField", FieldType.Integer),
                            new Field("BoolField", FieldType.Boolean),
                            new Field("DecimalField", FieldType.Decimal),
                            new Field("LongStringField", FieldType.LongString),
                            new Field("BinaryField", FieldType.Binary, 20)
                            )
                        );
                var upgrader = db.Init(DbUpgrader.Upgrade)
                                     .FromDefinition(definition)
                                     .LogToXunit(_output)
                                     .Build();

                Assert.True(upgrader.Run());

                db.AssertTableExists("OneOfEachDatabase", "OneOfEach");
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "StringField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "StringField", FieldType.String);
                db.AssertFieldSizeEquals("OneOfEachDatabase", "OneOfEach", "StringField", 20);
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "IntField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "IntField", FieldType.Integer);
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "BoolField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "BoolField", FieldType.Boolean);
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "DecimalField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "DecimalField", FieldType.Decimal);
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "LongStringField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "LongStringField", FieldType.LongString);
                db.AssertFieldExists("OneOfEachDatabase", "OneOfEach", "BinaryField");
                db.AssertFieldTypeEquals("OneOfEachDatabase", "OneOfEach", "BinaryField", FieldType.Binary);
                db.AssertFieldSizeEquals("OneOfEachDatabase", "OneOfEach", "BinaryField", 20);
            }
        }
    }
}