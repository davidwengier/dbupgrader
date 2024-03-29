﻿using DbUpgrader.Definition;

namespace DbUpgrader
{
    public interface IDestinationManager
    {
        bool TryDbConnect();

        bool DatabaseExists(string databaseName);

        void CreateDatabase(string databaseName);

        void SetDatabaseName(string dbName);

        bool TableExists(ITable table);

        void CreateTable(ITable table);

        bool FieldExists(ITable table, IField field);

        void CreateField(ITable table, IField field);

        IField GetFieldInfo(string tableName, string fieldName);

        void AlterField(ITable table, IField field);

        FieldType GetFieldTypeFromSourceType(string sourceType);
    }
}
