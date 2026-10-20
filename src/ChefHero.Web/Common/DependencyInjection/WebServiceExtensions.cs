using System.Security.Cryptography.X509Certificates;

using ChefHero.Web.Application.Authentication;
using ChefHero.Web.Infrastructure.Api.Authentication;
using ChefHero.Web.Infrastructure.Api.BringableKitchenItems;
using ChefHero.Web.Infrastructure.Authentication;
using ChefHero.Web.Infrastructure.Persistence;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

using ShadcnBlazor;

namespace ChefHero.Web.Common.DependencyInjection;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebComponents(
        this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        services.AddShadcnBlazor();

        return services;
    }

    public static IServiceCollection AddUserSession(
        this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserSession, CurrentUserSession>();

        return services;
    }

    public static IServiceCollection AddWebPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ChefHero")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:ChefHero is not configured for ChefHero.Web.");

        services.AddDbContext<WebSessionDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IWebSessionStore, EfWebSessionStore>();

        return services;
    }

    public static IServiceCollection AddWebAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        string keyRingPath = configuration["DataProtection:KeyRingPath"]
            ?? Path.Combine(environment.ContentRootPath, "App_Data", "DataProtectionKeys");

        Directory.CreateDirectory(keyRingPath);

        IDataProtectionBuilder dataProtection = services.AddDataProtection()
            .SetApplicationName("ChefHero.Web")
            .PersistKeysToFileSystem(new DirectoryInfo(keyRingPath));

        if (!environment.IsDevelopment())
        {
            string certificatePath = configuration["DataProtection:CertificatePath"]
                ?? throw new InvalidOperationException(
                    "DataProtection:CertificatePath must be configured outside Development.");
            string certificatePassword = configuration["DataProtection:CertificatePassword"]
                ?? throw new InvalidOperationException(
                    "DataProtection:CertificatePassword must be configured outside Development.");

            X509Certificate2 certificate = X509CertificateLoader.LoadPkcs12FromFile(
                certificatePath,
                certificatePassword);

            if (!certificate.HasPrivateKey)
            {
                certificate.Dispose();
                throw new InvalidOperationException(
                    "The Data Protection certificate must include its private key.");
            }

            services.AddSingleton(certificate);
            dataProtection.ProtectKeysWithCertificate(certificate);
        }

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "__Host-ChefHero.Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.Path = "/";
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/login";
            });

        services.AddAuthorization();
        services.AddCascadingAuthenticationState();

        return services;
    }

    public static IServiceCollection AddChefHeroApiClients(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        string? configuredBaseUrl = configuration["Api:BaseUrl"];
        if (!Uri.TryCreate(configuredBaseUrl, UriKind.Absolute, out Uri? apiBaseUri))
        {
            throw new InvalidOperationException(
                "Api:BaseUrl must be configured as an absolute URL.");
        }

        if (!environment.IsDevelopment() && apiBaseUri.Scheme != Uri.UriSchemeHttps)
        {
            throw new InvalidOperationException(
                "Api:BaseUrl must use HTTPS outside Development.");
        }

        services.AddHttpClient<BringableKitchenItemApiClient>(client =>
        {
            client.BaseAddress = apiBaseUri;
        });

        services.AddHttpClient<AuthApiClient>(client =>
        {
            client.BaseAddress = apiBaseUri;
        });

        return services;
    }
}
