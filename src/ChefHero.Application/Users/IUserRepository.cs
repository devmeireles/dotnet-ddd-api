using ChefHero.Domain.User;

namespace ChefHero.Application.Users;

public interface IUserRepository
{
    User? GetByEmail(Email email);
    void Add(User user);
    void SaveChanges();
}