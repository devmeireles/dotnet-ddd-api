namespace ChefHero.API.BringableKitchenItem;

public class PagedBringableKitchenItemResponse
{
    public required IReadOnlyCollection<BringableKitchenItemResponse> Items { get; init; }

    public required int CurrentPage { get; init; }

    public required int PageSize { get; init; }

    public required int TotalCount { get; init; }

    public required int TotalPages { get; init; }

    public required bool HasPrevious { get; init; }

    public required bool HasNext { get; init; }
}