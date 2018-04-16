using System;
using System.Collections.Generic;
using System.Linq;
using DbUpgrader.Definition;

namespace DbUpgrader.Tests
{
    internal class MemoryDatabase : IDestinationManager
    {
        private Dictionary<string, Table> _tables = new Dictionary<string, Table>(StringComparer.Ordinal);

        public Table[] Tables => _tables.Values.ToArray();

        public void CreateField(ITable table, IField field)
        {
            _tables[table.Name].AddField(new Field(field.Name, field.Type, field.Size));
        }

        public void CreateTable(ITable table)
        {
            _tables.Add(table.Name, new Table(table.Name, table.GetFields().ToArray()));
        }

        public bool FieldExists(ITable table, IField field)
        {
            return _tables[table.Name].Fields.Any(f => f.Name.Equals(field.Name, StringComparison.Ordinal));
        }

        public bool TableExists(ITable table) => _tables.ContainsKey(table.Name);

        public bool TryDbConnect() => true;
    }
}