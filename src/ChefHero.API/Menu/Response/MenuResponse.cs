
namespace ChefHero.API.Menu.Response;

public class MenuResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int MinPeople { get; init; }
    public required int MaxPeople { get; init; }
    public required decimal Price { get; init; }
}