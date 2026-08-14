using ChefHero.Application.Users;
using ChefHero.Domain.User;

using ChefHero.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Infrastructure.Users;

public class UserRepository : IUserRepository
{
    private readonly ChefHeroDbContext _dbContext;

    public UserRepository(ChefHeroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(
                user => user.Email == email,
                cancellationToken);
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(
            user,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}