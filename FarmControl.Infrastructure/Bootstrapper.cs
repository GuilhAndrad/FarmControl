using FarmControl.Domain.Extension;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmControl.Infrastructure;
public static class Bootstrapper
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMifrator(services, configurationManager);
    }
    private static void AddFluentMifrator(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(c =>
        c.AddMySql5()
        .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("FarmControl.Infrastructure")).For.All()
        );
    }
}
