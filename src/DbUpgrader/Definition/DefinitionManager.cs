namespace DbUpgrader.Definition
{
    internal class DefinitionManager : ISourceManager
    {
        private readonly IDatabase _database;

        public DefinitionManager(IDatabase database)
        {
            _database = database;
        }

        public IDatabase DatabaseDefinition => _database;
    }
}
