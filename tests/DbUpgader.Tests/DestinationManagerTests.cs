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
                var definition = new Database("MyDatabase",
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

                db.AssertTableExists("MyDatabase", "OneOfEach");
                db.AssertFieldExists("MyDatabase", "OneOfEach", "StringField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "StringField", FieldType.String);
                db.AssertFieldSizeEquals("MyDatabase", "OneOfEach", "StringField", 20);
                db.AssertFieldExists("MyDatabase", "OneOfEach", "IntField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "IntField", FieldType.Integer);
                db.AssertFieldExists("MyDatabase", "OneOfEach", "BoolField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "BoolField", FieldType.Boolean);
                db.AssertFieldExists("MyDatabase", "OneOfEach", "DecimalField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "DecimalField", FieldType.Decimal);
                db.AssertFieldExists("MyDatabase", "OneOfEach", "LongStringField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "LongStringField", FieldType.LongString);
                db.AssertFieldExists("MyDatabase", "OneOfEach", "BinaryField");
                db.AssertFieldTypeEquals("MyDatabase", "OneOfEach", "BinaryField", FieldType.Binary);
                db.AssertFieldSizeEquals("MyDatabase", "OneOfEach", "BinaryField", 20);
            }
        }
    }
}