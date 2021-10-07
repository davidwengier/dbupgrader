using System;
using System.IO;

namespace DbUpgrader.Tests.Sqlite
{
    internal class SqliteTestRun : IDisposable
    {
        private readonly string _fileName;

        public SqliteTestRun(string fileName)
        {
            _fileName = fileName;
        }

        public void Dispose()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }
    }
}
