using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

namespace ChefHero.Application.BringableKitchenItem;

public interface IBringableKitchenItemRepository
{
    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);

    Task<BringableKitchenItemEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IEnumerable<BringableKitchenItemEntity>> GetAllAsync(
        CancellationToken cancellationToken);

    Task AddAsync(
        BringableKitchenItemEntity item,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}