namespace ChefHero.Application.BringableKitchenItem;

public class BringableKitchenItemResult
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}