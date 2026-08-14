using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

namespace ChefHero.Application.BringableKitchenItem;

public interface IBringableKitchenItemRepository
{
    Task<BringableKitchenItemEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<BringableKitchenItemEntity>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<int> GetCountAsync(
        CancellationToken cancellationToken);

    Task AddAsync(
        BringableKitchenItemEntity item,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);
}