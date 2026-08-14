using ChefHero.Application.Auth.Password;
using ChefHero.Application.Auth.Token;
using ChefHero.Application.BringableKitchenItem;
using ChefHero.Application.Users;

using ChefHero.Infrastructure.Auth.Password;
using ChefHero.Infrastructure.Auth.Token;
using ChefHero.Infrastructure.BringableKitchenItem;
using ChefHero.Infrastructure.Persistence;
using ChefHero.Infrastructure.Users;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.API.Common.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IBringableKitchenItemRepository, BringableKitchenItemRepository>();

        services.AddDbContext<ChefHeroDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("ChefHero"));
            });

        return services;
    }
}