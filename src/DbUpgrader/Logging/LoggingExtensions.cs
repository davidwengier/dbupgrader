using DbUpgrader.Logging;

namespace DbUpgrader
{
    public static class LoggingExtensions
    {
        public static UpgraderBuilder LogToConsole(this UpgraderBuilder builder)
        {
            builder.Logger = new ConsoleLogger();
            return builder;
        }
    }
}
