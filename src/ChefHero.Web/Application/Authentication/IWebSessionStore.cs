namespace ChefHero.Web.Application.Authentication;

public interface IWebSessionStore
{
    Task CreateAsync(
        Guid sessionId,
        Guid userId,
        string protectedAccessToken,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default);

    Task<string?> GetProtectedAccessTokenAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default);

    Task RevokeAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default);
}
