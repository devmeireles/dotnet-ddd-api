using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.Menu.Request;

public class MenuRequest
{
    [Required]
    [StringLength(64, MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(256)]
    public required string Description { get; set; }

    [Required]
    [Range(1, 100)]
    public required int MinPeople { get; set; }

    [Required]
    [Range(1, 100)]
    public required int MaxPeople { get; set; }

    [Required]
    [Range(0.01, 100000)]
    public required decimal Price { get; set; }
}