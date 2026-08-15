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
            null,
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
                true,
                cancellationToken);

        return item is null
            ? null
            : ToResult(item);
    }

    public async Task<PagedBringableKitchenItemResult> GetAllAsync(
        int page,
        int pageSize,
        string? searchTerm,
        bool? isActive,
        CancellationToken cancellationToken)
    {
        IEnumerable<BringableKitchenItemEntity> items =
            await _repository.GetAllAsync(
                page,
                pageSize,
                searchTerm,
                isActive,
                cancellationToken);

        int totalCount =
            await _repository.GetCountAsync(
                searchTerm,
                isActive,
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

    public async Task<BringableKitchenItemResult?> PatchAsync(
    Guid id,
    PatchBringableKitchenItemCommand command,
    CancellationToken cancellationToken)
    {
        if (!command.HasName &&
            !command.HasDescription)
        {
            throw new ValidationException(
                "At least one property must be provided.");
        }

        BringableKitchenItemEntity? item =
            await _repository.GetByIdAsync(
                id,
                true,
                cancellationToken);

        if (item is null)
        {
            return null;
        }

        string name = command.HasName
            ? command.Name!
            : item.Name;

        string? description = command.HasDescription
            ? command.Description
            : item.Description;

        if (command.HasName)
        {
            bool nameExists =
                await _repository.ExistsByNameAsync(
                    name,
                    item.Id,
                    cancellationToken);

            if (nameExists)
            {
                throw new ConflictException(
                    "A kitchen item with this name already exists.");
            }
        }

        item.Update(
            name,
            description);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return ToResult(item);
    }

    public async Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemEntity? item =
            await _repository.GetByIdAsync(
                id,
                false,
                cancellationToken);

        if (item is null)
        {
            return false;
        }

        item.Activate();

        await _repository.SaveChangesAsync(
            cancellationToken);

        return true;
    }

    public async Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        BringableKitchenItemEntity? item =
            await _repository.GetByIdAsync(
                id,
                true,
                cancellationToken);

        if (item is null)
        {
            return false;
        }

        item.Deactivate();

        await _repository.SaveChangesAsync(
            cancellationToken);

        return true;
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