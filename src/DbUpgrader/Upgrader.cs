using System;
using System.Collections.Generic;
using DbUpgrader.Definition;
using DbUpgrader.Logging;

namespace DbUpgrader
{
    public class Upgrader
    {
        public ILogger Logger { get; internal set; }
        public IDestinationManager DestinationManager { get; internal set; }
        public ISourceManager SourceManager { get; internal set; }

        public bool Run()
        {
            try
            {
                LogInfo("Starting upgrade");

                LogInfo("Trying to connect to destination database");
                if (!this.DestinationManager.TryDbConnect())
                {
                    LogError("Couldn't connect to destination database");
                    return false;
                }
                LogInfo("Connected successfully");

                LogInfo("Checking if the database exists");
                var dbName = this.SourceManager.DatabaseDefinition.GetDatabaseName();
                if (!this.DestinationManager.DatabaseExists(dbName))
                {
                    LogInfo("Database doesn't exist, creating");
                    this.DestinationManager.CreateDatabase(dbName);
                }

                LogInfo("Setting database name");
                this.DestinationManager.SetDatabaseName(dbName);

                foreach (var table in this.SourceManager.DatabaseDefinition.GetTables())
                {
                    UpgradeTable(table);
                }

                LogInfo("Finished upgade successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogError("Upgrade failed due to an unexpected exception: " + ex.ToString());
                LogInfo("Finished upgade unsuccessfully");
                return false;
            }
        }

        private void UpgradeTable(ITable table)
        {
            LogInfo("Upgrading table " + table.Name + ". Checking if it exists");
            if (!this.DestinationManager.TableExists(table))
            {
                LogInfo("Table doesn't exist, creating");
                this.DestinationManager.CreateTable(table);
            }
            else
            {
                LogInfo("Table exists, checking fields");
                foreach (var field in table.GetFields())
                {
                    UpgradeField(table, field);
                }
            }
        }

        private void UpgradeField(ITable table, IField field)
        {
            LogInfo("Upgrading field " + field.Name + " in " + table.Name + ". Checking if it exists");
            if (!this.DestinationManager.FieldExists(table, field))
            {
                LogInfo("Field doesn't exist, creating");
                this.DestinationManager.CreateField(table, field);
            }
            else
            {
                LogInfo("Field exists, checking properties");
                UpdateFieldProperties(table, field);
            }
        }

        private void UpdateFieldProperties(ITable table, IField field)
        {
            var info = this.DestinationManager.GetFieldInfo(table.Name, field.Name);
            var reasons = new List<string>();
            if (info.Size != field.Size)
            {
                reasons.Add("Size changed from " + info.Size + " to " + field.Size);
            }
            if (info.Type != field.Type)
            {
                reasons.Add("Type changed from " + info.Type + " to " + field.Type);
            }
            if (info.AllowNulls != field.AllowNulls)
            {
                reasons.Add("AllowNulls changed from " + info.AllowNulls + " to " + field.AllowNulls);
            }
            if (reasons.Count > 0)
            {
                LogInfo("Field needs changing: " + string.Join(", ", reasons));
                this.DestinationManager.AlterField(table, field);
            }
        }

        private void LogInfo(string message)
        {
            this.Logger?.Log(LogLevel.Information, message);
        }

        private void LogError(string message)
        {
            this.Logger?.Log(LogLevel.Error, message);
        }
    }
}
