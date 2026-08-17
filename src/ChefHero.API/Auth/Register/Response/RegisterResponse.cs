namespace ChefHero.API.Auth.Register.Response;

public class RegisterResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}