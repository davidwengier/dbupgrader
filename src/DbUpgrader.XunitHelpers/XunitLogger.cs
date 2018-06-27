using System;
using DbUpgrader.Logging;
using Xunit.Abstractions;

namespace DbUpgrader.XunitHelpers
{
    internal class XunitLogger : ILogger
    {
        private ITestOutputHelper _output;

        public XunitLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Log(LogLevel level, string message)
        {
            _output.WriteLine(level.ToString() + ": " + message);
            if (level == LogLevel.Error)
            {
                throw new Exception(message);
            }
        }
    }
}