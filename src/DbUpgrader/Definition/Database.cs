using System;
using System.Collections.Generic;
using System.Linq;

namespace DbUpgrader.Definition
{
    public class Database : IDatabase
    {
        private List<ITable> _tables = new List<ITable>();

        public Database(params ITable[] tables)
        {
            _tables.AddRange(tables);
        }

        public IEnumerable<ITable> GetTables() => _tables;
    }
}