using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.Auth.Login.Request;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [StringLength(128)]
    public required string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public required string Password { get; set; }
}