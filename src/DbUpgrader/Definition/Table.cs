using System;
using System.Collections.Generic;

namespace DbUpgrader.Definition
{
    public class Table : ITable
    {
        private List<IField> _fields = new List<IField>();

        public Table(string name, params IField[] fields)
        {
            this.Name = name;
            _fields.AddRange(fields);
        }

        public string Name { get; }
            
        public void AddField(IField field) => _fields.Add(field);

        public IEnumerable<IField> GetFields() => _fields;

        public void RemoveField(IField field)
        {
            _fields.Remove(field);
        }
    }
}