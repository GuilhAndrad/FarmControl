using FarmControl.Application.Services.Cryptography;
using FarmControl.Application.Services.Token;
using FarmControl.Application.Services.UserLogged;
using FarmControl.Application.UseCase.User.Register;
using FarmControl.Application.UseCases.User.ChangePassword;
using FarmControl.Application.UseCases.User.Login.ToDoLogin;
using FarmControl.Communication.UseCases.User.Login.ToDoLogin;
using FarmControl.Communication.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmControl.Application;
public static class Bootstraper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAditionalSecurityKey(services, configuration);
        AddTokenJWT(services, configuration);
        AddHashIds(services, configuration);
        AddUseCases(services);
        AddUserLogged(services);
    }

    private static void AddUserLogged(IServiceCollection services)
    {
        services.AddScoped<IUserLogged, UserLogged>();
    }

    //This code adds an additional security key to the IServiceCollection services.
    //It retrieves the required section from the configuration and adds a PasswordEncryptor object to the services.
    private static void AddAditionalSecurityKey(IServiceCollection services, IConfiguration configuration)
    {
        //Retrieve the required section from the configuration
        var section = configuration.GetRequiredSection("Configurations:Password:PasswordAdditionalKey");

        //Add a PasswordEncryptor object to the services
        services.AddScoped(option => new PasswordEncryptor(section.Value));
    }
    //This code adds a JWT token to the service collection.
    private static void AddTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        //Retrieve the token lifetime and token key from the configuration section.
        var sectionLifeTime = configuration.GetRequiredSection("Configurations:Jwt:TokenLifetimeMinutes");
        var sectionKey = configuration.GetRequiredSection("Configurations:Jwt:TokenKey");

        //Add the token controller to the service collection with the lifetime and key values.
        services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }

    private static void AddHashIds(IServiceCollection services, IConfiguration configuration)
    {
        var salt = configuration.GetRequiredSection("Configurations:HashIds:Salt");
        services.AddHashids(setup =>
        {
            setup.Salt = salt.Value;
            setup.MinHashLength = 3;
        });
    }

    //This code adds three use cases to the service collection.
    private static void AddUseCases(IServiceCollection services)
    {
        //Add RegisterUserUseCase to the service collection with a scoped lifetime
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()

        //Add LoginUseCase to the service collection with a scoped lifetime
        .AddScoped<ILoginUseCase, LoginUseCase>()

        //Add ChangePasswordUseCase to the service collection with a scoped lifetime
        .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
    }
}