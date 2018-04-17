using System;
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

                foreach (ITable table in this.SourceManager.DatabaseDefinition.GetTables())
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
            if (!this.DestinationManager.TableExists(table))
            {
                this.DestinationManager.CreateTable(table);
            }
            else
            {
                foreach (IField field in table.GetFields())
                {
                    UpgradeField(table, field);
                }
            }
        }

        private void UpgradeField(ITable table, IField field)
        {
            if (!this.DestinationManager.FieldExists(table, field))
            {
                this.DestinationManager.CreateField(table, field);
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