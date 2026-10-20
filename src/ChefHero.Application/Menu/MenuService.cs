
using ChefHero.Application.Common.Exceptions;
using ChefHero.Application.Common.Pagination;

using Microsoft.Extensions.Logging;

using MenuEntity =
    ChefHero.Domain.Menu.Menu;

namespace ChefHero.Application.Menu;

public class MenuService : IMenuService
{
    private const string DuplicateNameMessage =
        "A menu with this name already exists.";

    private readonly IMenuRepository _repository;
    private readonly ILogger<MenuService> _logger;

    public MenuService(
        IMenuRepository repository,
        ILogger<MenuService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken)
    {
        MenuEntity? menu = await _repository.GetByIdAsync(
            id,
            false,
            cancellationToken);

        if (menu is null)
        {
            return false;
        }

        menu.Activate();

        await _repository.SaveChangesAsync(
            cancellationToken);

        _logger.LogInformation("Activated menu {MenuId}.", id);
        return true;
    }

    public async Task<MenuResult> CreateAsync(MenuCommand command, CancellationToken cancellationToken)
    {
        bool exists = await _repository.ExistsByNameAsync(
            command.Name,
            null,
            cancellationToken);

        if (exists)
        {
            throw new ConflictException(DuplicateNameMessage);
        }

        MenuEntity menu =
            MenuEntity.Create(
                command.Name,
                command.Description,
                command.MinPeople,
                command.MaxPeople,
                command.Price);

        await _repository.AddAsync(
            menu,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        _logger.LogInformation("Created menu {MenuId}.", menu.Id);
        return menu.ToResult();
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        MenuEntity? menu = await _repository.GetByIdAsync(
            id,
            true,
            cancellationToken);

        if (menu is null)
        {
            return false;
        }

        menu.Deactivate();

        await _repository.SaveChangesAsync(
            cancellationToken);

        _logger.LogInformation("Deactivated menu {MenuId}.", id);
        return true;
    }

    public async Task<PagedResult<MenuResult>> GetAllAsync(int page, int pageSize, string? searchTerm, bool? isActive, CancellationToken cancellationToken)
    {
        IEnumerable<MenuEntity> items = await _repository.GetAllAsync(
            page,
            pageSize,
            searchTerm,
            isActive,
            cancellationToken);

        int totalCount = await _repository.GetCountAsync(
            searchTerm,
            isActive,
            cancellationToken);

        PagedResult<MenuResult> response = new PagedResult<MenuResult>
        {
            Items = items
                .Select(item => item.ToResult())
                .ToList(),

            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return response;
    }

    public async Task<MenuResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        MenuEntity? menu =
            await _repository.GetByIdAsync(
                id,
                true,
                cancellationToken);

        return menu is null
            ? null
            : menu.ToResult();
    }

    public async Task<MenuResult?> PatchAsync(
        Guid id,
        PatchMenuCommand command,
        CancellationToken cancellationToken)
    {
        if (!command.HasName &&
            !command.HasDescription &&
            !command.HasMinPeople &&
            !command.HasMaxPeople &&
            !command.HasPrice)
        {
            throw new ValidationException(
                "At least one property must be provided.");
        }

        MenuEntity? menu = await _repository.GetByIdAsync(
            id,
            true,
            cancellationToken);

        if (menu is null)
        {
            return null;
        }

        string name = command.HasName
            ? command.Name ?? throw new ValidationException("Name cannot be null.")
            : menu.Name;
        string description = command.HasDescription
            ? command.Description ?? throw new ValidationException("Description cannot be null.")
            : menu.Description;
        int minPeople = command.HasMinPeople
            ? command.MinPeople ?? throw new ValidationException("MinPeople cannot be null.")
            : menu.MinPeople;
        int maxPeople = command.HasMaxPeople
            ? command.MaxPeople ?? throw new ValidationException("MaxPeople cannot be null.")
            : menu.MaxPeople;
        decimal price = command.HasPrice
            ? command.Price ?? throw new ValidationException("Price cannot be null.")
            : menu.Price;

        if (command.HasName &&
            await _repository.ExistsByNameAsync(
                name,
                menu.Id,
                cancellationToken))
        {
            throw new ConflictException(DuplicateNameMessage);
        }

        menu.Update(
            name,
            description,
            minPeople,
            maxPeople,
            price);

        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated menu {MenuId}.", id);
        return menu.ToResult();
    }
}