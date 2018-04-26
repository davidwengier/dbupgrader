using System.Text;
using DbUpgrader.Definition;

namespace DbUpgrader.Generators
{
    public static class SqlGenerator
    {
        public static string GenerateCreateTableStatement(ISqlGenerator generator, ITable table)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE [" + table.Name + "] (");
            foreach (IField field in table.GetFields())
            {
                sb.Append("[");
                sb.Append(field.Name);
                sb.Append("] ");
                sb.Append(generator.GetFieldDataType(field));
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}