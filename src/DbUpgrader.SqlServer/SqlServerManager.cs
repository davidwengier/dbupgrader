using DbUpgrader.Definition;

namespace DbUpgrader
{
    internal class SqlServerManager : IDestinationManager
    {
        private string _connectionString;

        public SqlServerManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateField(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }

        public void CreateTable(ITable table)
        {
            throw new System.NotImplementedException();
        }

        public bool FieldExists(ITable table, IField field)
        {
            throw new System.NotImplementedException();
        }

        public bool TableExists(ITable table)
        {
            throw new System.NotImplementedException();
        }

        public bool TryDbConnect()
        {
            throw new System.NotImplementedException();
        }
    }
}