namespace ChefHero.Web.Infrastructure.Api.Authentication;

public sealed record LoginApiRequest(string Email, string Password);

public sealed record LoginApiResponse(
    Guid Id,
    string Name,
    string Email,
    string Token);
