﻿using System.Text;
using DbUpgrader.Definition;

namespace DbUpgrader.Generators
{
    public static class SqlGenerator
    {
        public static string GenerateCreateTableStatement(ISqlGenerator generator, ITable table)
        {
            var sb = new StringBuilder();
            sb.Append("CREATE TABLE " + generator.GetDatabaseObjectEscapeStartChar() + table.Name + generator.GetDatabaseObjectEscapeEndChar() + " (");
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

        public static string GenerateCreateFieldStatement(ISqlGenerator generator, ITable table, IField field)
        {
            return GenerateAlterTableAndFieldStatement(generator, table, field, "ADD");
        }

        public static string GenerateAlterFieldStatement(ISqlGenerator generator, ITable table, IField field)
        {
            return GenerateAlterTableAndFieldStatement(generator, table, field, "ALTER COLUMN");
        }

        public static string GenerateAlterTableAndFieldStatement(ISqlGenerator generator, ITable table, IField field, string operation)
        {
            var sb = new StringBuilder();
            sb.Append("ALTER TABLE " + generator.GetDatabaseObjectEscapeStartChar() + table.Name + generator.GetDatabaseObjectEscapeEndChar() + " ");
            sb.Append(operation);
            sb.Append(' ');
            sb.Append(GenerateFieldProperties(generator, field));
            return sb.ToString();
        }

        public static string GenerateFieldProperties(ISqlGenerator generator, IField field)
        {
            var sb = new StringBuilder();
            sb.Append(generator.GetDatabaseObjectEscapeStartChar());
            sb.Append(field.Name);
            sb.Append(generator.GetDatabaseObjectEscapeEndChar());
            sb.Append(' ');
            sb.Append(generator.GetFieldDataType(field));
            sb.Append(' ');
            sb.Append(field.AllowNulls ? "NULL" : "NOT NULL");
            return sb.ToString();
        }
    }
}
