using System;
using DbUpgrader.Definition;

namespace DbUpgrader.Tests
{
    internal static class TestData
    {
        internal static Database CreateSimpleDatabaseDefinition()
        {
            return new Database("MyDatabase",
                        new Table(
                            "MyTable",
                            new Field("MyField", FieldType.String, 20)
                            )
                        );
        }
    }
}