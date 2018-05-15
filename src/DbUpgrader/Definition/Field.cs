using System;

namespace DbUpgrader.Definition
{
    public class Field : IField
    {
        public Field(string name, FieldType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public Field(string name, FieldType type, int size)
            : this(name, type)
        {
            this.Size = size;
        }

        public string Name { get; set; }

        public FieldType Type { get; set; }

        public int Size { get; set; }

        public bool AllowNulls { get; set; }
    }
}