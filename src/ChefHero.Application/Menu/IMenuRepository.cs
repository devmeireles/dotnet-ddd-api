using MenuEntity =
    ChefHero.Domain.Menu.Menu;

namespace ChefHero.Application.Menu;

public interface IMenuRepository
{
    Task<MenuEntity?> GetByIdAsync(
        Guid id,
        bool? isActive,
        CancellationToken cancellationToken);

    Task<IEnumerable<MenuEntity>> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken);

    Task<int> GetCountAsync(
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken);

    Task AddAsync(
        MenuEntity menu,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        Guid? excludeId,
        CancellationToken cancellationToken);
}