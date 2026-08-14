using ChefHero.Application.Users;
using ChefHero.Domain.User;
using ChefHero.Infrastructure.Persistence;
namespace ChefHero.Infrastructure.Users;

public class UserRepository : IUserRepository
{
    private readonly ChefHeroDbContext _dbContext;

    public UserRepository(ChefHeroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetByEmail(Email email)
    {
        return _dbContext.Users
            .FirstOrDefault(user => user.Email == email);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}