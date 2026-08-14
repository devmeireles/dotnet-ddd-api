namespace ChefHero.Application.Auth.Register;

public class RegisterCommand
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Phone { get; init; }
    public required string AddressLine { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string ZipCode { get; init; }
}