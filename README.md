# DbUpgrader

An all purpose database upgrader that doesn't rely on you writing your own scripts.

This project is designed to help with ensuring that database changes are rolled out successfully, either from testing to production, or from source code to anywhere. The aim is to not require developers manually write migration scripts and not require any existing known state before being able to migrate. Running DbUpgrader will make your destination database schema match your expectations no matter what its starting point is, or even whether it exists at all.

The simplest (at this stage theoretical) example code to run DbUpgrader is:

```C#
string connectionString = "...";
string definition = @"<database name=""MyDatabase"">
                          <table name=""MyTable"">
                              <field name=""MyField"" type=""String"" size=""50"" />
                          </table>
                      </database>";

var upgrader = DbUpgrader.Upgrade
                         .FromXmlDefinition(definition)
                         .ToSqlServer(connectionString)
                         .Build();

bool result = upgrader.Run();
```

DbUpgrader will look at the supplied definition and ensure that there is a table called MyTable with a single field called MyField, which is can store a string of length 50 characters, once it has finished. If that means creating a table, or adding a field, or removing a few unused field, or resizing a field, it will work it out.

The aim is to have a seamless process for database upgrades where the definition is completely reviewable in source control just like any other file, creating a new database is as easy as upgrading an existing one, and the developer doesn't have to think in terms of "migrations", ie the changes necessary to take a database from one state to another, simply in terms of the database definition as a whole.

Other options are planned for other scenarios like:

```C#
// Make production schema match testing
DbUpgrader.Upgrade
          .FromSqlServer(testingConnectionString, "DatabaseName")
          .ToSqlServer(productionConnectionString)
          .Run();

// Supply definition with a POCO
var db = new Database(new Table(...));
DbUpgrader.Upgrade
          .FromDefinition(db)
          .ToSqlServer(connectionString)
          .Run();

// Migrate from one database technology to another
DbUpgrader.Upgrade
          .FromMySql(mySqlConnectionString, "DatabaseName")
          .ToSqlServer(sqlServerConnectionString)
          .Run();
```

At least thats the idea. If you're reading this then hopefully there is actual code in the repository.