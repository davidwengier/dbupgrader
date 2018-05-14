using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace DbUpgrader.Tests.Unit
{
    public class UpgraderTests
    {
        private ITestOutputHelper _output;

        public UpgraderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void EmptyDatabase_TableAndField_Added()
        {
            var database = new InMemoryDatabase();
            var upgader = DbUpgrader.Upgrade
                                    .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                    .To(database)
                                    .LogToXunit(_output)
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
            var database = new InMemoryDatabase();
            database.CreateTable(TestData.CreateSimpleDatabaseDefinition().GetTables().First());
            database.DenyChanges = true;
            var upgader = DbUpgrader.Upgrade
                                    .FromDefinition(TestData.CreateSimpleDatabaseDefinition())
                                    .To(database)
                                    .LogToXunit(_output)
                                    .Build();

            Assert.True(upgader.Run());
        }
    }
}