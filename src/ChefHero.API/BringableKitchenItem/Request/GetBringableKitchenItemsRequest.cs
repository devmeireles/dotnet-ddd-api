using ChefHero.API.Common.Requests;

namespace ChefHero.API.BringableKitchenItem.Request;

public class GetBringableKitchenItemsRequest : PagedSearchRequest
{
    public bool? IsActive { get; set; } = true;
}