using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.Common.Requests;

public class PagedSearchRequest
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 20;

    public string? SearchTerm { get; set; }
}