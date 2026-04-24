using System.ComponentModel.DataAnnotations;

namespace auth_service.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(256)]
    public required string Email { get; set; }

    [MaxLength(512)]
    public required string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsApproved { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
