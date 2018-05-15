using System.Text;
using DbUpgrader.Definition;

namespace DbUpgrader.Generators
{
    public static class SqlGenerator
    {
        public static string GenerateCreateTableStatement(ISqlGenerator generator, ITable table)
        {
            var sb = new StringBuilder();
            sb.Append("CREATE TABLE [" + table.Name + "] (");
            var prependComma = false;
            foreach (var field in table.GetFields())
            {
                if (prependComma)
                {
                    sb.Append(',');
                }
                sb.Append(GenerateFieldProperties(generator, field));
                prependComma = true;
            }
            sb.Append(")");
            return sb.ToString();
        }

        public static string GenerateAlterFieldStatement(ISqlGenerator generator, ITable table, IField field)
        {
            var sb = new StringBuilder();
            sb.Append("ALTER TABLE [" + table.Name + "] ");
            sb.Append("ALTER COLUMN ");
            sb.Append(GenerateFieldProperties(generator, field));
            return sb.ToString();
        }

        public static string GenerateFieldProperties(ISqlGenerator generator, IField field)
        {
            var sb = new StringBuilder();
            sb.Append('[');
            sb.Append(field.Name);
            sb.Append(']');
            sb.Append(' ');
            sb.Append(generator.GetFieldDataType(field));
            sb.Append(' ');
            sb.Append(field.AllowNulls ? "NULL" : "NOT NULL");
            return sb.ToString();
        }
    }
}