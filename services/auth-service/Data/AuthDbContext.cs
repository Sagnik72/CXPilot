using auth_service.Models;
using Microsoft.EntityFrameworkCore;

namespace auth_service.Data;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        user.ToTable("Users");
        user.HasKey(x => x.Id);
        user.HasIndex(x => x.Email).IsUnique();
        user.Property(x => x.Email).IsRequired();
        user.Property(x => x.Name).IsRequired();
        user.Property(x => x.PasswordHash).IsRequired();
        user.Property(x => x.Role).HasConversion<string>().HasMaxLength(20);
        user.Property(x => x.CreatedAtUtc).HasDefaultValueSql("GETUTCDATE()");
    }
}
