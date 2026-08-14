namespace ChefHero.Application.Common.Security;

public interface ICurrentUser
{
    Guid UserId { get; }
    string Email { get; }
    string Role { get; }
}