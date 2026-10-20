namespace ChefHero.Application.Menu;

public class MenuResult
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int MinPeople { get; init; }
    public required int MaxPeople { get; init; }
    public required decimal Price { get; init; }
}