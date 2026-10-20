using ChefHero.Application.Common.Pagination;

namespace ChefHero.Application.Menu;

public interface IMenuService
{
    Task<MenuResult> CreateAsync(
        MenuCommand command,
        CancellationToken cancellationToken);

    Task<MenuResult?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PagedResult<MenuResult>> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken);

    Task<MenuResult?> PatchAsync(
        Guid id,
        PatchMenuCommand command,
        CancellationToken cancellationToken);

    Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken);
}