using System.ComponentModel.DataAnnotations;

namespace ticket_service.Models;

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(150)]
    public required string Title { get; set; }

    [MaxLength(4000)]
    public required string Description { get; set; }

    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    [MaxLength(300)]
    public required string GithubRepo { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid? AssignedToUserId { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
