using System.Collections.Generic;

namespace DbUpgrader.Definition
{
    public class Database : IDatabase
    {
        private readonly List<ITable> _tables = new List<ITable>();
        private readonly string _databaseName;

        public Database(string databaseName, params ITable[] tables)
        {
            _databaseName = databaseName;
            _tables.AddRange(tables);
        }

        public string GetDatabaseName() => _databaseName;

        public IEnumerable<ITable> GetTables() => _tables;
    }
}
