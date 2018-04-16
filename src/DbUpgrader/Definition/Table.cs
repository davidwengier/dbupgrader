using System;
using System.Collections.Generic;
using System.Linq;

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
        public IField[] Fields => _fields.ToArray();

        public void AddField(IField field) => _fields.Add(field);

        public IEnumerable<IField> GetFields() => _fields;
    }
}