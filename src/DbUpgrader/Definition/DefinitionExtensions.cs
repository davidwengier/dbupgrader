using System;
using System.Linq;

namespace DbUpgrader.Definition
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