namespace ChefHero.Web.Application.Authentication;

public interface ICurrentUserSession
{
    Task<string?> GetAccessTokenAsync(
        CancellationToken cancellationToken = default);
}