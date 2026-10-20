namespace ChefHero.Application.Menu;

public sealed record PatchMenuCommand
(
    string? Name,
    string? Description,
    int? MinPeople,
    int? MaxPeople,
    decimal? Price,
    bool HasName,
    bool HasDescription,
    bool HasMinPeople,
    bool HasMaxPeople,
    bool HasPrice
);