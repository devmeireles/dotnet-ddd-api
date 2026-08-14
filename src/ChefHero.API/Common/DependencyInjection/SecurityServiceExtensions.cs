using ChefHero.API.Common.Security;

using ChefHero.Application.Common.Security;

namespace ChefHero.API.Common.DependencyInjection;

public static class SecurityServiceExtensions
{
    public static IServiceCollection AddSecurityServices(
        this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}