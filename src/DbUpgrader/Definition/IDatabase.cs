using System;
using System.Collections.Generic;
using System.Linq;

namespace DbUpgrader.Definition
{
    /// <summary>
    /// Represents a database. See also the Database class which provides a simple concrete implementation
    /// </summary>
    public interface IDatabase
    {
        string GetDatabaseName();

        IEnumerable<ITable> GetTables();
    }
}