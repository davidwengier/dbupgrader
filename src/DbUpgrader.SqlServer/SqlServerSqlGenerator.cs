using System;
using DbUpgrader.Definition;
using DbUpgrader.Generators;

namespace DbUpgrader.SqlServer
{
    internal class SqlServerSqlGenerator : ISqlGenerator
    {
        char ISqlGenerator.GetDatabaseObjectEscapeStartChar()
        {
            return '[';
        }

        char ISqlGenerator.GetDatabaseObjectEscapeEndChar()
        {
            return ']';
        }

        string ISqlGenerator.GetFieldDataType(IField field)
        {
            switch (field.Type)
            {
                case FieldType.String when field.Size <= 0:
                {
                    return "VARCHAR(MAX)";
                }
                case FieldType.String:
                {
                    return "VARCHAR(" + field.Size + ")";
                }
                case FieldType.LongString:
                {
                    return "TEXT";
                }
                case FieldType.Integer:
                {
                    return "INT";
                }
                case FieldType.Boolean:
                {
                    return "BIT";
                }
                case FieldType.Decimal:
                {
                    return "DECIMAL";
                }
                case FieldType.Binary:
                {
                    return "BINARY";
                }
            }
            throw new Exception("Field type " + field.Type + " is not supported.");
        }
    }
}