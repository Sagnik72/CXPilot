using auth_service.Data;
using auth_service.Dtos;
using auth_service.Models;
using auth_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auth_service.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    AuthDbContext dbContext,
    IJwtTokenService jwtTokenService,
    ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var alreadyExists = await dbContext.Users
            .AnyAsync(u => u.Email == normalizedEmail, ct);
        if (alreadyExists)
        {
            return Conflict(new { message = "Email is already registered." });
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = normalizedEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = UserRole.User,
            IsApproved = false
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetPendingUsers), new { id = user.Id }, new
        {
            user.Id,
            user.Name,
            user.Email,
            user.IsApproved,
            Role = user.Role.ToString()
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await dbContext.Users
            .SingleOrDefaultAsync(u => u.Email == normalizedEmail, ct);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        if (!user.IsApproved)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "User is not approved by admin yet." });
        }

        var (token, expiresAtUtc) = jwtTokenService.GenerateToken(user);

        var response = new AuthResponse
        {
            Token = token,
            ExpiresAtUtc = expiresAtUtc,
            Email = user.Email,
            Role = user.Role.ToString()
        };

        return Ok(response);
    }

    [HttpGet("users/pending")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetPendingUsers(CancellationToken ct)
    {
        var users = await dbContext.Users
            .Where(u => !u.IsApproved)
            .OrderBy(u => u.CreatedAtUtc)
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.CreatedAtUtc
            })
            .ToListAsync(ct);

        return Ok(users);
    }

    [HttpPost("approve-user")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> ApproveUser([FromBody] ApproveUserRequest request, CancellationToken ct)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, ct);
        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        user.IsApproved = true;
        user.Role = request.Role;

        await dbContext.SaveChangesAsync(ct);
        logger.LogInformation("User {UserId} approved with role {Role}", user.Id, user.Role);

        return Ok(new
        {
            user.Id,
            user.Email,
            user.IsApproved,
            Role = user.Role.ToString()
        });
    }
}
