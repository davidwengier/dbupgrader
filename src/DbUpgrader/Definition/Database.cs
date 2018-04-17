using System;
using System.Collections.Generic;
using System.Linq;

namespace DbUpgrader.Definition
{
    public class Database : IDatabase
    {
        private List<ITable> _tables = new List<ITable>();
        private string _databaseName;

        public Database(string databaseName, params ITable[] tables)
        {
            _databaseName = databaseName;
            _tables.AddRange(tables);
        }

        public string GetDatabaseName() => _databaseName;

        public IEnumerable<ITable> GetTables() => _tables;
    }
}