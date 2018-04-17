using System;
using System.Linq;
using DbUpgrader.Definition;

namespace DbUpgrader.Tests
{
    public static class Program
    {
        public static void Main()
        {
            string connectionString = @"Server=(localdb)\v11.0;Integrated Security=true;";
            IDatabase definition = new Database("MyDatabase",
                                    new Table("MyTable",
                                        new Field("MyField", FieldType.String, 50)
                                        )
                                    );

            Upgrader upgrader = DbUpgrader.Upgrade
                                          .FromDefinition(definition)
                                          .ToSqlServer(connectionString)
                                          .LogToConsole()
                                          .Build();

            upgrader.Run();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}