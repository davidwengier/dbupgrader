using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
