namespace ChefHero.Application.BringableKitchenItem;

public interface IBringableKitchenItemService
{
    Task<BringableKitchenItemResult> CreateAsync(
        BringableKitchenItemCommand command,
        CancellationToken cancellationToken);

    Task<BringableKitchenItemResult?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PagedBringableKitchenItemResult> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<BringableKitchenItemResult?> UpdateAsync(
        Guid id,
        BringableKitchenItemCommand command,
        CancellationToken cancellationToken);
}