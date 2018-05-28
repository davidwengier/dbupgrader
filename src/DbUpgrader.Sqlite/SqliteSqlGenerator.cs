using DbUpgrader.Generators;
using DbUpgrader.Definition;
using System;

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
            throw new Exception("Field type " + field + " is not supported.");
        }
    }
}