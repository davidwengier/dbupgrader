using System;

namespace DbUpgrader.Definition
{
    public class Field : IField
    {
        public Field(string name, FieldType type, int size)
        {
            this.Name = name;
            this.Type = type;
            this.Size = size;
        }

        public string Name { get; }

        public FieldType Type { get; }

        public int Size { get; }
    }
}