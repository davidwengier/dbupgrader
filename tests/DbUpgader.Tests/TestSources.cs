using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUpgrader.Tests
{
    public static class TestSources
    {
        public static IEnumerable<object[]> GetTestSources()
        {
            yield return new object[] { new InMemory.InMemoryHelper() };

            var sqlServer = new SqlServer.SqlServerHelper(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;");
            if (sqlServer.ShouldRun())
            {
                yield return new object[] { sqlServer };
            }

            var mySql = new MySql.MySqlHelper("Server=localhost;Uid=test;Pwd=test;SslMode=none;");
            if (mySql.ShouldRun())
            {
                yield return new object[] { mySql };
            }

            var sqlite = new Sqlite.SqliteHelper("MyDatabase.db");
            if (sqlite.ShouldRun())
            {
                yield return new object[] { sqlite };
            }
        }
    }
}
