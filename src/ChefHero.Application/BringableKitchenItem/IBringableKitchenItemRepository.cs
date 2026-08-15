using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

namespace ChefHero.Application.BringableKitchenItem;

public interface IBringableKitchenItemRepository
{
    Task<BringableKitchenItemEntity?> GetByIdAsync(
        Guid id,
        bool? isActive,
        CancellationToken cancellationToken);

    Task<IEnumerable<BringableKitchenItemEntity>> GetAllAsync(
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
        BringableKitchenItemEntity item,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        Guid? excludeId,
        CancellationToken cancellationToken);
}