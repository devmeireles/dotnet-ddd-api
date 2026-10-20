using System.Security.Claims;
using System.Security.Cryptography;

using ChefHero.Web.Application.Authentication;
using ChefHero.Web.Infrastructure.Persistence;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;

namespace ChefHero.Web.Infrastructure.Authentication;

public sealed class CurrentUserSession(
    AuthenticationStateProvider authenticationStateProvider,
    IWebSessionStore sessionStore,
    IDataProtectionProvider dataProtectionProvider) : ICurrentUserSession
{
    private readonly IDataProtector _tokenProtector =
        dataProtectionProvider.CreateProtector("ChefHero.Web.ApiAccessToken.v1");

    public async Task<string?> GetAccessTokenAsync(
        CancellationToken cancellationToken = default)
    {
        ClaimsPrincipal user = (await authenticationStateProvider
            .GetAuthenticationStateAsync()).User;

        string? sessionClaim = user.FindFirstValue("web_session_id");
        if (!Guid.TryParse(sessionClaim, out Guid sessionId))
        {
            return null;
        }

        string? protectedToken = await sessionStore.GetProtectedAccessTokenAsync(
            sessionId,
            cancellationToken);

        if (protectedToken is null)
        {
            return null;
        }

        try
        {
            return _tokenProtector.Unprotect(protectedToken);
        }
        catch (CryptographicException exception)
        {
            throw new InvalidOperationException(
                "The stored API access token could not be decrypted. Check the Data Protection key ring.",
                exception);
        }
    }
}