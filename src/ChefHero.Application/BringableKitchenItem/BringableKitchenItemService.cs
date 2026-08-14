using ChefHero.Application.Common.Exceptions;

using BringableKitchenItemEntity =
    ChefHero.Domain.BringableKitchenItem.BringableKitchenItem;

namespace ChefHero.Application.BringableKitchenItem;

public class BringableKitchenItemService : IBringableKitchenItemService
{
    private readonly IBringableKitchenItemRepository _repository;

    public BringableKitchenItemService(
        IBringableKitchenItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<BringableKitchenItemResult> CreateAsync(
        BringableKitchenItemCommand command,
        CancellationToken cancellationToken)
    {
        bool exists = await _repository.ExistsByNameAsync(
            command.Name,
            cancellationToken);

        if (exists)
        {
            throw new ConflictException(
                "A kitchen item with this name already exists.");
        }

        BringableKitchenItemEntity item =
            BringableKitchenItemEntity.Create(
                command.Name,
                command.Description);

        await _repository.AddAsync(
            item,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return ToResult(item);
    }

    public async Task<BringableKitchenItemResult?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemEntity? item =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        return item is null
            ? null
            : ToResult(item);
    }

    public async Task<PagedBringableKitchenItemResult> GetAllAsync(
    int page,
    int pageSize,
    CancellationToken cancellationToken)
    {
        IEnumerable<BringableKitchenItemEntity> items =
            await _repository.GetAllAsync(
                page,
                pageSize,
                cancellationToken);

        int totalCount =
            await _repository.GetCountAsync(
                cancellationToken);

        return new PagedBringableKitchenItemResult
        {
            Items = items
                .Select(ToResult)
                .ToList(),

            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<BringableKitchenItemResult?> UpdateAsync(
        Guid id,
        BringableKitchenItemCommand command,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemEntity? item =
            await _repository.GetByIdAsync(
                id,
                cancellationToken);

        if (item is null)
        {
            return null;
        }

        bool nameExists = await _repository.ExistsByNameAsync(
            command.Name,
            cancellationToken);

        if (nameExists && item.Name != command.Name)
        {
            throw new ConflictException(
                "A kitchen item with this name already exists.");
        }

        item.Update(
            command.Name,
            command.Description);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return ToResult(item);
    }

    private static BringableKitchenItemResult ToResult(
        BringableKitchenItemEntity item)
    {
        return new BringableKitchenItemResult
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description ?? string.Empty
        };
    }
}