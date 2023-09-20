using Dapper;
using MySqlConnector;

namespace FarmControl.Infrastructure.Migrations;
public static class Database
{
    public static void CreateDatabase(string dataBaseConnection, string databaseName)
    {
        using var myConnection = new MySqlConnection(dataBaseConnection);

        var parameters = new DynamicParameters();

        parameters.Add("name", databaseName);

        var records = myConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

        if (!records.Any())
        {
            myConnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
