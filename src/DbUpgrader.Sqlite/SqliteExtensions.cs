using System;

namespace DbUpgrader
{
    public static class SqliteExtensions
    {
        public static UpgraderBuilder ToSqlite(this UpgraderBuilder builder, string connectionString)
        {
            builder.DestinationManager = new SqliteManager(connectionString);
            return builder;
        }
    }
}