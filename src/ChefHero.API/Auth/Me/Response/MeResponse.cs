namespace ChefHero.API.Auth.Me.Mapper;

public class MeResponse
{
    public required Guid Id { get; init; }

    public required string Email { get; init; }

    public required string Role { get; init; }
}