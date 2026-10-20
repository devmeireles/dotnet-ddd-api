namespace ChefHero.Application.Common.Pagination;

public class PagedResult<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }

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