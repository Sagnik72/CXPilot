using Microsoft.EntityFrameworkCore;
using ticket_service.Models;

namespace ticket_service.Data;

public class TicketDbContext(DbContextOptions<TicketDbContext> options) : DbContext(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ticket = modelBuilder.Entity<Ticket>();
        ticket.ToTable("Tickets");
        ticket.HasKey(x => x.Id);
        ticket.Property(x => x.Title).IsRequired();
        ticket.Property(x => x.Description).IsRequired();
        ticket.Property(x => x.GithubRepo).IsRequired();
        ticket.Property(x => x.Priority).HasConversion<string>().HasMaxLength(20);
        ticket.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        ticket.Property(x => x.CreatedAtUtc).HasDefaultValueSql("GETUTCDATE()");
        ticket.Property(x => x.UpdatedAtUtc).HasDefaultValueSql("GETUTCDATE()");
        ticket.HasIndex(x => x.Status);
        ticket.HasIndex(x => x.Priority);
        ticket.HasIndex(x => x.CreatedByUserId);
    }
}
