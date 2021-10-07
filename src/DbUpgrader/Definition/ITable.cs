using System.Collections.Generic;

namespace DbUpgrader.Definition
{
    public interface ITable
    {
        string Name { get; }

        IEnumerable<IField> GetFields();
    }
}
