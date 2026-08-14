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
        bool nameExists = await _repository.ExistsByNameAsync(
            command.Name,
            cancellationToken);

        if (nameExists)
        {
            throw new ConflictException(
                $"A kitchen item with name '{command.Name}' already exists.");
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

    public async Task<IEnumerable<BringableKitchenItemResult>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        IEnumerable<BringableKitchenItemEntity> items =
            await _repository.GetAllAsync(
                cancellationToken);

        return items.Select(ToResult);
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
            Description = item.Description
        };
    }
}