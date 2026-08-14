using System.ComponentModel.DataAnnotations;

namespace ChefHero.API.Auth.Register;

public class RegisterRequest
{
    [Required]
    [StringLength(64, MinimumLength = 4)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(128)]
    public required string Email { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public required string Password { get; set; }

    [Required]
    [StringLength(32)]
    public required string Phone { get; set; }

    [Required]
    [StringLength(128)]
    public required string AddressLine { get; set; }

    [Required]
    [StringLength(64)]
    public required string City { get; set; }

    [Required]
    [StringLength(64)]
    public required string State { get; set; }

    [Required]
    [StringLength(16)]
    public required string ZipCode { get; set; }
}

