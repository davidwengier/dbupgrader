﻿using System;
using System.Collections.Generic;
using System.Linq;
using DbUpgrader.Definition;

namespace DbUpgrader.Tests.InMemory
{
    internal class InMemoryDatabase : IDestinationManager
    {
        private readonly Dictionary<string, Table> _tables = new(StringComparer.Ordinal);

        public Table[] Tables => _tables.Values.ToArray();

        public bool DenyChanges { get; internal set; }

        public void SetDatabaseName(string dbName)
        {
            // nop
        }

        public void CreateDatabase(string databaseName)
        {
            // nop
        }

        public void CreateField(ITable table, IField field)
        {
            if (this.DenyChanges)
            {
                throw new InvalidOperationException("This database does not allow changes");
            }
            _tables[table.Name].AddField(new Field(field.Name, field.Type, field.Size));
        }

        public void CreateTable(ITable table)
        {
            if (this.DenyChanges)
            {
                throw new InvalidOperationException("This database does not allow changes");
            }
            _tables.Add(table.Name, new Table(table.Name, table.GetFields().ToArray()));
        }

        public bool DatabaseExists(string databaseName) => true;

        public bool FieldExists(ITable table, IField field)
        {
            return _tables[table.Name].GetFields().Any(f => f.Name.Equals(field.Name, StringComparison.Ordinal));
        }

        public bool TableExists(ITable table) => _tables.ContainsKey(table.Name);

        public bool TryDbConnect() => true;

        public IField GetFieldInfo(string tableName, string fieldName)
        {
            return _tables[tableName].GetFields().FirstOrDefault(f => f.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        public void AlterField(ITable table, IField field)
        {
            _tables[table.Name].RemoveField(GetFieldInfo(table.Name, field.Name));
            _tables[table.Name].AddField(field);
        }

        public FieldType GetFieldTypeFromSourceType(string sourceType)
        {
            return (FieldType)Enum.Parse(typeof(FieldType), sourceType, true);
        }
    }
}
