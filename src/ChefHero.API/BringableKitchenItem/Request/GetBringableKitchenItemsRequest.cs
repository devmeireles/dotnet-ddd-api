using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.BringableKitchenItem.Request;

public class GetBringableKitchenItemsRequest
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 20;

    public string? SearchTerm { get; set; }

    public bool? IsActive { get; set; } = true;
}