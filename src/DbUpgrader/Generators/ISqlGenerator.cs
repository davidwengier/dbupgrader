using DbUpgrader.Definition;

namespace DbUpgrader.Generators
{
    public interface ISqlGenerator
    {
        char GetDatabaseObjectEscapeStartChar();
        char GetDatabaseObjectEscapeEndChar();
        string GetFieldDataType(IField field);
    }
}
