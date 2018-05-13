using DbUpgrader.Generators;
using DbUpgrader.Definition;

namespace DbUpgrader.SqlServer
{
	internal class SqlServerSqlGenerator : ISqlGenerator
	{
        string ISqlGenerator.GetFieldDataType(IField field)
        {
            switch (field.Type)
            {
                case FieldType.String when field.Size > 0:
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