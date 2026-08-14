namespace ChefHero.Application.Auth.Login;

public class LoginResult
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Token { get; init; }
}