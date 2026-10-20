using ChefHero.Application.Menu;
using ChefHero.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using MenuEntity =
    ChefHero.Domain.Menu.Menu;

namespace ChefHero.Infrastructure.Menu;

public class MenuRepository : IMenuRepository
{
    private readonly ChefHeroDbContext _dbContext;

    public MenuRepository(
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

        return await _dbContext.Menus
            .AnyAsync(
                item =>
                    item.IsActive &&
                    item.Name.ToLower() == normalizedName &&
                    (!excludeId.HasValue ||
                     item.Id != excludeId.Value),
                cancellationToken);
    }

    public async Task<MenuEntity?> GetByIdAsync(
        Guid id,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IQueryable<MenuEntity> query =
            _dbContext.Menus.AsTracking();

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

    public async Task<IEnumerable<MenuEntity>> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IQueryable<MenuEntity> query =
            _dbContext.Menus
            .AsNoTracking();

        if (isActive.HasValue)
        {
            query = query.Where(
                item => item.IsActive == isActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string normalizedSearchTerm = searchTerm.Trim().ToLowerInvariant();

            query = query.Where(
                item => item.Name.ToLower().Contains(normalizedSearchTerm));
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
        IQueryable<MenuEntity> query =
            _dbContext.Menus;

        if (isActive.HasValue)
        {
            query = query.Where(
                item => item.IsActive == isActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string normalizedSearchTerm = searchTerm.Trim().ToLowerInvariant();

            query = query.Where(
                item => item.Name.ToLower().Contains(normalizedSearchTerm));
        }

        return await query.CountAsync(cancellationToken);
    }

    public async Task AddAsync(
        MenuEntity item,
        CancellationToken cancellationToken)
    {
        await _dbContext.Menus.AddAsync(
            item,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}