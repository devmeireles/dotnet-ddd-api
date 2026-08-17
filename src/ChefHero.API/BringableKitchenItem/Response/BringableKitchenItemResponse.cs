namespace ChefHero.API.BringableKitchenItem.Response;

public class BringableKitchenItemResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}