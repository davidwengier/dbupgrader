using System;
using DbUpgrader.Definition;
using DbUpgrader.Generators;

namespace DbUpgrader.Sqlite
{
    internal class SqliteSqlGenetator : ISqlGenerator
    {
        char ISqlGenerator.GetDatabaseObjectEscapeStartChar()
        {
            return ' ';
        }
        char ISqlGenerator.GetDatabaseObjectEscapeEndChar()
        {
            return ' ';
        }

        string ISqlGenerator.GetFieldDataType(IField field)
        {
            switch (field.Type)
            {
                case FieldType.String:
                {
                    return "TEXT";
                }
            }
            throw new Exception("Field type " + field.Type + " is not supported.");
        }
    }
}
