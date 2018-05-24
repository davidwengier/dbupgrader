using System;

namespace DbUpgrader
{
    public static class MySqlExtensions
    {
        public static UpgraderBuilder ToMySql(this UpgraderBuilder builder, string connectionString)
        {
            builder.DestinationManager = new MySqlManager(connectionString);
            return builder;
        }
    }
}