namespace auth_service.Dtos;

public class AuthResponse
{
    public required string Token { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
