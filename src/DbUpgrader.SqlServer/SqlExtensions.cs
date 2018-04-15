using System;

namespace DbUpgrader
{
    public static class SqlExtensions
    {
        public static UpgraderBuilder ToSqlServer(this UpgraderBuilder builder, string connectionString)
        {
            builder.DestinationManager = new SqlServerManager(connectionString);
            return builder;
        }
    }
}