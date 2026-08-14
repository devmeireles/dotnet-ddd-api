using ChefHero.Application.Auth.Register;
using ChefHero.Application.Auth.Login;
using ChefHero.Application.Auth.Me;

namespace ChefHero.API.Common.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IMeService, MeService>();

        return services;
    }
}