using DbUpgrader.Definition;
using DbUpgrader.Generators;

namespace DbUpgrader.MySql
{
    internal class MySqlGenerator : ISqlGenerator
    {
        char ISqlGenerator.GetDatabaseObjectEscapeStartChar()
        {
            return '`';
        }

        char ISqlGenerator.GetDatabaseObjectEscapeEndChar()
        {
            return '`';
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
            }
            return null;
        }
    }
}