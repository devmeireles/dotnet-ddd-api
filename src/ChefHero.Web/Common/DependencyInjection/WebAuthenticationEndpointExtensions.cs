using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using ChefHero.Web.Application.Authentication;
using ChefHero.Web.Infrastructure.Api.Authentication;

using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

namespace ChefHero.Web.Common.DependencyInjection;

public static class WebAuthenticationEndpointExtensions
{
    private const string SessionIdClaim = "web_session_id";

    public static IEndpointRouteBuilder MapWebAuthenticationEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/account/login", LoginAsync)
            .AllowAnonymous();

        endpoints.MapPost("/account/logout", LogoutAsync)
            .AllowAnonymous();

        return endpoints;
    }

    private static async Task<IResult> LoginAsync(
        HttpContext httpContext,
        IAntiforgery antiforgery,
        AuthApiClient authApiClient,
        IWebSessionStore sessionStore,
        IDataProtectionProvider dataProtectionProvider,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        await antiforgery.ValidateRequestAsync(httpContext);

        IFormCollection form = await httpContext.Request.ReadFormAsync(cancellationToken);
        string email = form["Email"].ToString();
        string password = form["Password"].ToString();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return Results.Redirect("/login?error=invalid");
        }

        LoginApiResponse login;
        try
        {
            login = await authApiClient.LoginAsync(
                email,
                password,
                cancellationToken);
        }
        catch (HttpRequestException exception)
            when (exception.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                  exception.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return Results.Redirect("/login?error=invalid");
        }
        catch (HttpRequestException exception)
        {
            loggerFactory.CreateLogger("ChefHero.Web.Authentication").LogWarning(
                exception,
                "The configured authentication API could not complete a login request.");
            return Results.Redirect("/login?error=unavailable");
        }

        DateTimeOffset expiresAt = new JwtSecurityTokenHandler()
            .ReadJwtToken(login.Token)
            .ValidTo;

        if (expiresAt <= DateTimeOffset.UtcNow)
        {
            return Results.Redirect("/login?error=unavailable");
        }

        Guid sessionId = Guid.NewGuid();
        IDataProtector tokenProtector = dataProtectionProvider
            .CreateProtector("ChefHero.Web.ApiAccessToken.v1");

        await sessionStore.CreateAsync(
            sessionId,
            login.Id,
            tokenProtector.Protect(login.Token),
            expiresAt,
            cancellationToken);

        ClaimsIdentity identity = new(
        [
            new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
            new Claim(ClaimTypes.Name, login.Name),
            new Claim(ClaimTypes.Email, login.Email),
            new Claim(SessionIdClaim, sessionId.ToString())
        ],
        CookieAuthenticationDefaults.AuthenticationScheme);

        AuthenticationProperties properties = new()
        {
            IsPersistent = true,
            AllowRefresh = false,
            ExpiresUtc = expiresAt
        };

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            properties);

        return Results.Redirect("/bringable-kitchen-items");
    }

    private static async Task<IResult> LogoutAsync(
        HttpContext httpContext,
        IAntiforgery antiforgery,
        IWebSessionStore sessionStore,
        CancellationToken cancellationToken)
    {
        await antiforgery.ValidateRequestAsync(httpContext);

        string? sessionClaim = httpContext.User.FindFirstValue(SessionIdClaim);
        if (Guid.TryParse(sessionClaim, out Guid sessionId))
        {
            await sessionStore.RevokeAsync(sessionId, cancellationToken);
        }

        await httpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Redirect("/login?loggedOut=true");
    }
}
