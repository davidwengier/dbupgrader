using DbUpgrader.Definition;

namespace DbUpgrader
{
    public interface ISourceManager
    {
        IDatabase DatabaseDefinition { get; }
    }
}