using DbUpgrader.Generators;
using DbUpgrader.Definition;

namespace DbUpgrader.Sqlite
{
    internal class SqliteSqlGenetator : ISqlGenerator
    {
        string ISqlGenerator.GetFieldDataType(IField field)
        {
            switch (field.Type)
            {
                case FieldType.String:
                {
                    return "TEXT";
                }
            }
            return null;
        }
    }
}