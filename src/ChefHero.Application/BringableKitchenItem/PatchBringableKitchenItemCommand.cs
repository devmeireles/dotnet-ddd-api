namespace ChefHero.Application.BringableKitchenItem;

public sealed record PatchBringableKitchenItemCommand(
    string? Name,
    string? Description,
    bool HasName,
    bool HasDescription);