namespace ChefHero.Application.Auth.Login;

public class LoginCommand
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}