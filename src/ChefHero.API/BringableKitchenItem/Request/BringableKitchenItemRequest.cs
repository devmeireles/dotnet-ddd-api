using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.BringableKitchenItem.Request;

public class BringableKitchenItemRequest
{
    [Required]
    [StringLength(64, MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }
}