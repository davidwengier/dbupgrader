﻿using System;
using DbUpgrader.Definition;
using Microsoft.Data.Sqlite;

namespace DbUpgrader.Tests.Sqlite
{
    internal class Assert
    {
        internal static void TableExists(string connectionString, string tableName)
        {
            var sql = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @tableName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqliteParameter("tableName", tableName))) == 0)
            {
                throw new Exception("Table '" + tableName + "' does not exist.");
            }
        }

        internal static void FieldExists(string connectionString, string tableName, string fieldName)
        {
            var sql = "SELECT COUNT(*) AS CNTREC FROM pragma_table_info(@tableName) WHERE name=@fieldName";
            if (Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqliteParameter("tableName", tableName), new SqliteParameter("fieldName", fieldName))) == 0)
            {
                throw new Exception("Field '" + fieldName + "' doesn't exist in '" + tableName + "' does not exist.");
            }
        }

        internal static void FieldSizeEquals(int size, string connectionString, string tableName, string fieldName)
        {
            var sql = "SELECT CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            var actual = Convert.ToInt32(ExecuteScalar(connectionString, sql, new SqliteParameter("tableName", tableName), new SqliteParameter("fieldName", fieldName)));
            if (size != actual)
            {
                throw new Exception("Field '" + fieldName + "' in table '" + tableName + "' is not " + size + " characters long, its " + actual);
            }
        }

        internal static void FieldTypeEquals(FieldType type, string connectionString, string tableName, string fieldName)
        {
            var sql = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @fieldName";
            var actual = ExecuteScalar(connectionString, sql, new SqliteParameter("tableName", tableName), new SqliteParameter("fieldName", fieldName)).ToString();
            var actualType = SqliteManager.GetFieldType(actual);
            if (type != actualType)
            {
                throw new Exception("Field '" + fieldName + "' in table '" + tableName + "' is not a " + type + ", its " + actual + " (" + actualType + ")");
            }
        }

        private static object ExecuteScalar(string connectionString, string sql, params SqliteParameter[] parameters)
        {
            using var conn = new SqliteConnection(connectionString);
            using var comm = new SqliteCommand();
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = sql;
            comm.Parameters.AddRange(parameters);
            return comm.ExecuteScalar();
        }
    }
}
