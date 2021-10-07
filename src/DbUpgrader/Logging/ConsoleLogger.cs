using System;

namespace DbUpgrader.Logging
{
    internal class ConsoleLogger : ILogger
    {
        void ILogger.Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.DbChange:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                }
                case LogLevel.Error:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                }
                default:
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
            }
            Console.WriteLine(message);
        }
    }
}
