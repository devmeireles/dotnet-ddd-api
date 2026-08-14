namespace ChefHero.Application.BringableKitchenItem;

public class PagedBringableKitchenItemResult
{
    public required IReadOnlyCollection<BringableKitchenItemResult> Items { get; init; }

    public required int CurrentPage { get; init; }

    public required int PageSize { get; init; }

    public required int TotalCount { get; init; }

    public int TotalPages =>
        PageSize == 0
            ? 0
            : (int)Math.Ceiling(
                (double)TotalCount / PageSize);

    public bool HasPrevious =>
        CurrentPage > 1;

    public bool HasNext =>
        CurrentPage < TotalPages;
}