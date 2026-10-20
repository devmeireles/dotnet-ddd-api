using ChefHero.Web.Application.Authentication;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Web.Infrastructure.Persistence;

public sealed class EfWebSessionStore(
    WebSessionDbContext dbContext) : IWebSessionStore
{
    public async Task CreateAsync(
        Guid sessionId,
        Guid userId,
        string protectedAccessToken,
        DateTimeOffset expiresAt,
        CancellationToken cancellationToken = default)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;

        await dbContext.Sessions
            .Where(session =>
                session.ExpiresAt <= now ||
                session.RevokedAt < now.AddDays(-30))
            .ExecuteDeleteAsync(cancellationToken);

        dbContext.Sessions.Add(new WebSession
        {
            Id = sessionId,
            UserId = userId,
            ProtectedAccessToken = protectedAccessToken,
            CreatedAt = now,
            ExpiresAt = expiresAt
        });

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<string?> GetProtectedAccessTokenAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;

        return await dbContext.Sessions
            .AsNoTracking()
            .Where(session =>
                session.Id == sessionId &&
                session.RevokedAt == null &&
                session.ExpiresAt > now)
            .Select(session => session.ProtectedAccessToken)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task RevokeAsync(
        Guid sessionId,
        CancellationToken cancellationToken = default)
    {
        WebSession? session = await dbContext.Sessions
            .SingleOrDefaultAsync(
                item => item.Id == sessionId && item.RevokedAt == null,
                cancellationToken);

        if (session is null)
        {
            return;
        }

        session.RevokedAt = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
