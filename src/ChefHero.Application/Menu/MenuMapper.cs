using MenuEntity =
    ChefHero.Domain.Menu.Menu;

namespace ChefHero.Application.Menu;

public static class MenuMapper
{
    public static MenuResult ToResult(
        this MenuEntity menu)
    {
        return new MenuResult
        {
            Id = menu.Id,
            Name = menu.Name,
            Description = menu.Description,
            MinPeople = menu.MinPeople,
            MaxPeople = menu.MaxPeople,
            Price = menu.Price
        };
    }
}