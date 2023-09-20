using Microsoft.Extensions.Configuration;

namespace FarmControl.Domain.Extension;
public static class ExtensionRepository
{
    public static string GetNameDatabase(this IConfiguration configurationManager)
    {
        var databaseName = configurationManager.GetConnectionString("DataBaseName");
        return databaseName;
    }
    public static string GetConnection(this IConfiguration configurationManager)
    {
        var connection = configurationManager.GetConnectionString("Connection");
        return connection;
    }
    public static string GetFullConnection(this IConfiguration configurationManager)
    {
        var databaseName = configurationManager.GetNameDatabase();
        var connection = configurationManager.GetConnection();

        return $"{connection}Database={databaseName}";
    }
}
