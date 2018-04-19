using DbUpgrader.Definition;

namespace DbUpgrader.Generators
{
	public interface ISqlGenerator
	{
		string GetFieldDataType(IField field);
	}
}