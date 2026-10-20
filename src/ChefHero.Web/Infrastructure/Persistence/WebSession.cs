namespace ChefHero.Web.Infrastructure.Persistence;

public sealed class WebSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public required string ProtectedAccessToken { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset? RevokedAt { get; set; }
}
