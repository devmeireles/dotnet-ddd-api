using ChefHero.API.Menu.Request;
using ChefHero.API.Menu.Response;
using ChefHero.Application.Menu;
using ChefHero.Application.Common.Pagination;

namespace ChefHero.API.Menu;

public static class MenuMapper
{
    public static MenuCommand ToCommand(
        this MenuRequest request)
    {
        return new MenuCommand
        {
            Name = request.Name,
            Description = request.Description,
            MinPeople = request.MinPeople,
            MaxPeople = request.MaxPeople,
            Price = request.Price
        };
    }

    public static MenuResponse ToResponse(
        this MenuResult result)
    {
        return new MenuResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            MaxPeople = result.MaxPeople,
            MinPeople = result.MinPeople,
            Price = result.Price
        };
    }

    public static PagedMenuResponse ToResponse(
        this PagedResult<MenuResult> result)
    {
        return new PagedMenuResponse
        {
            Items = result.Items
                .Select(item => item.ToResponse())
                .ToList(),

            CurrentPage = result.CurrentPage,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages,
            HasPrevious = result.HasPrevious,
            HasNext = result.HasNext
        };
    }
}