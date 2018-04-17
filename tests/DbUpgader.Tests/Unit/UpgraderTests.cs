using System;
using System.Linq;
using Xunit;

namespace DbUpgrader.Tests.Unit
{
    public class UpgraderTests
    {
        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            InMemoryDatabase database = new InMemoryDatabase();
            var upgader = DbUpgrader.Upgrade
                    .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                    .To(database)
                    .Build();

            Assert.True(upgader.Run());

            Assert.Single(database.Tables);
            Assert.Equal("MyTable", database.Tables[0].Name);
            Assert.Single(database.Tables[0].Fields);
            Assert.Equal("MyField", database.Tables[0].Fields[0].Name);
        }

        [Fact]
        public void EmptyDatabase_TableAndField_NotAdded()
        {
            InMemoryDatabase database = new InMemoryDatabase();
            database.CreateTable(TestData.CreateSimpleDatabaseDefinition().GetTables().First());
            database.DenyChanges = true;
            var upgader = DbUpgrader.Upgrade
                    .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                    .To(database)
                    .Build();

            Assert.True(upgader.Run());
        }
    }
}