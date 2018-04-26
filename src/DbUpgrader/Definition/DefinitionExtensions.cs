using System;
using DbUpgrader.Definition;

namespace DbUpgrader
{
    public static class DefinitionExtensions
    {
        public static UpgraderBuilder FromDefinition(this UpgraderBuilder builder, IDatabase database)
        {
            builder.SourceManager = new DefinitionManager(database);
            return builder;
        }
    }
}