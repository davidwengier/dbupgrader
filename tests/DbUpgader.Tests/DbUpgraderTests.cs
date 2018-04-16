using System;
using System.Linq;
using DbUpgrader.Definition;
using Xunit;

namespace DbUpgrader.Tests
{
    public class SimpleTests
    {
        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            MemoryDatabase database = new MemoryDatabase();
            var upgader = DbUpgrader.Upgrade
                    .FromDefinition(new Database(
                        new Table(
                            "MyTable",
                            new Field("MyField", FieldType.String, 20)
                            )
                        )
                    )
                    .To(database)
                    .Build();

            upgader.Run();

            Assert.Single(database.Tables);
            Assert.Equal("MyTable", database.Tables[0].Name);
            Assert.Single(database.Tables[0].Fields);
            Assert.Equal("MyField", database.Tables[0].Fields[0].Name);
        }
    }
}