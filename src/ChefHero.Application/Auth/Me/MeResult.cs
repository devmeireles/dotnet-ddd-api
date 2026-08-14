namespace ChefHero.Application.Auth.Me;

public class MeResult
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
}