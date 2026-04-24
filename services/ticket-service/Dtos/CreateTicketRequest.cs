using System.ComponentModel.DataAnnotations;
using ticket_service.Models;

namespace ticket_service.Dtos;

public class CreateTicketRequest
{
    [Required]
    [MaxLength(150)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(4000)]
    public required string Description { get; set; }

    [Required]
    [EnumDataType(typeof(TicketPriority))]
    public TicketPriority Priority { get; set; }

    [Required]
    [MaxLength(300)]
    public required string GithubRepo { get; set; }

    [Required]
    public Guid CreatedByUserId { get; set; }
}
