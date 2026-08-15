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
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken);

    Task<BringableKitchenItemResult?> UpdateAsync(
        Guid id,
        UpdateBringableKitchenItemCommand command,
        CancellationToken cancellationToken);

    Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken);
}