﻿using DbUpgrader.Definition;

namespace DbUpgrader
{
    public interface IDestinationManager
    {
        bool TryDbConnect();

        bool TableExists(ITable table);

        void CreateTable(ITable table);
        bool FieldExists(ITable table, IField field);
        void CreateField(ITable table, IField field);
    }
}