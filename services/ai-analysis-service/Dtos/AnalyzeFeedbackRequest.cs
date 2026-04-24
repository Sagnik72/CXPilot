using System.ComponentModel.DataAnnotations;

namespace ai_analysis_service.Dtos;

public class AnalyzeFeedbackRequest
{
    [Required]
    [MaxLength(2000)]
    public required string Text { get; set; }
}
