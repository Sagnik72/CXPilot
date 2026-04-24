using System.ComponentModel.DataAnnotations;

namespace auth_service.Dtos;

public class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(64)]
    public required string Password { get; set; }
}
