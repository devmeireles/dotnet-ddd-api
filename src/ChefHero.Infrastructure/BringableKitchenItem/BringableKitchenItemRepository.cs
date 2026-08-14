using ChefHero.Application.BringableKitchenItem;
using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

using ChefHero.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace ChefHero.Infrastructure.BringableKitchenItem;

public class BringableKitchenItemRepository
    : IBringableKitchenItemRepository
{
    private readonly ChefHeroDbContext _dbContext;

    public BringableKitchenItemRepository(
        ChefHeroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByNameAsync(
    string name,
    CancellationToken cancellationToken)
    {
        return await _dbContext.BringableKitchenItems
            .AnyAsync(
                item => item.Name == name,
                cancellationToken);
    }

    public async Task<BringableKitchenItemEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.BringableKitchenItems
            .FirstOrDefaultAsync(
                item => item.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<BringableKitchenItemEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.BringableKitchenItems
            .Where(item => item.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        BringableKitchenItemEntity item,
        CancellationToken cancellationToken)
    {
        await _dbContext.BringableKitchenItems.AddAsync(
            item,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}