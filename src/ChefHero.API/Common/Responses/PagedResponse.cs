namespace ChefHero.API.Common.Responses;

public class PagedResponse<T>
{
    public IReadOnlyCollection<T> Items { get; init; } = [];
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPrevious =>
        CurrentPage > 1;

    public bool HasNext =>
        CurrentPage < TotalPages;
}