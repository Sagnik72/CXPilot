using System.ComponentModel.DataAnnotations;

namespace ticket_service.Dtos;

public class SubmitFeedbackRequest
{
    [Required]
    [MaxLength(2000)]
    public required string Message { get; set; }
}
