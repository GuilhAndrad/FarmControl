using FarmControl.Domain.Extension;
using FarmControl.Domain.Repositories;
using FarmControl.Domain.Repositories.User;
using FarmControl.Infrastructure.AccessRepositories;
using FarmControl.Infrastructure.Repository;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmControl.Infrastructure;
public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMifrator(services, configurationManager);
        AddContext(services, configurationManager);
        AddRepositorios(services);
        AddUnitOfWork(services);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
        var connectionString = configurationManager.GetFullConnection();

        services.AddDbContext<FarmControlContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

    }
    private static void AddFluentMifrator(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddFluentMigratorCore().ConfigureRunner(c =>
        c.AddMySql5()
        .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("FarmControl.Infrastructure")).For.All()
        );
    }
}
