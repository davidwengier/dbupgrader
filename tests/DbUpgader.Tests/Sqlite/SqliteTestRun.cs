using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace DbUpgrader.Tests.Sqlite
{
    internal class SqliteTestRun : IDisposable
    {
        private string _fileName;

        public SqliteTestRun(string fileName)
        {
            _fileName = fileName;
        }

#pragma warning disable CA1063 // Implement IDisposable Correctly

        public void Dispose()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

#pragma warning restore CA1063 // Implement IDisposable Correctly
    }
}