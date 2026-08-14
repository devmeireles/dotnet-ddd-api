namespace ChefHero.Application.Auth.Token;

public interface ITokenService
{
    string Generate(Guid userId, string email, string role);
}