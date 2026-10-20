namespace ChefHero.API.Menu.Response;

public class PagedMenuResponse
{
    public required IReadOnlyCollection<MenuResponse> Items { get; init; }

    public required int CurrentPage { get; init; }

    public required int PageSize { get; init; }

    public required int TotalCount { get; init; }

    public required int TotalPages { get; init; }

    public required bool HasPrevious { get; init; }

    public required bool HasNext { get; init; }
}