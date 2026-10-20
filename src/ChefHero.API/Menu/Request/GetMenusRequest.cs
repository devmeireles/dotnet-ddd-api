using ChefHero.API.Common.Requests;

namespace ChefHero.API.Menu.Request;

public class GetMenusRequest : PagedSearchRequest
{
    public bool? IsActive { get; set; } = true;
}