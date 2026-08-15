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
        Guid? excludeId,
        CancellationToken cancellationToken)
    {
        string normalizedName = name.Trim().ToLowerInvariant();

        return await _dbContext.BringableKitchenItems
            .AnyAsync(
                item =>
                    item.IsActive &&
                    item.Name.ToLower() == normalizedName &&
                    (!excludeId.HasValue ||
                     item.Id != excludeId.Value),
                cancellationToken);
    }

    public async Task<BringableKitchenItemEntity?> GetByIdAsync(
        Guid id,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IQueryable<BringableKitchenItemEntity> query =
            _dbContext.BringableKitchenItems;

        if (isActive.HasValue)
        {
            query = query.Where(
                item => item.IsActive == isActive.Value);
        }

        return await query
            .FirstOrDefaultAsync(
                item => item.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<BringableKitchenItemEntity>> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IQueryable<BringableKitchenItemEntity> query =
            _dbContext.BringableKitchenItems
                .AsNoTracking();

        if (isActive.HasValue)
        {
            query = query.Where(
                item => item.IsActive == isActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string normalizedSearchTerm =
                searchTerm.Trim().ToLowerInvariant();

            query = query.Where(
                item => item.Name
                    .ToLower()
                    .Contains(normalizedSearchTerm));
        }

        return await query
            .OrderBy(item => item.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IQueryable<BringableKitchenItemEntity> query =
            _dbContext.BringableKitchenItems;

        if (isActive.HasValue)
        {
            query = query.Where(
                item => item.IsActive == isActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string normalizedSearchTerm =
                searchTerm.Trim().ToLowerInvariant();

            query = query.Where(
                item => item.Name
                    .ToLower()
                    .Contains(normalizedSearchTerm));
        }

        return await query.CountAsync(cancellationToken);
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