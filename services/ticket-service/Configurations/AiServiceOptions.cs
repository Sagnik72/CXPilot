namespace ticket_service.Configurations;

public class AiServiceOptions
{
    public const string SectionName = "AiService";

    public required string BaseUrl { get; set; }
    public string AnalyzeFeedbackPath { get; set; } = "/api/analyze-feedback";
    public int TimeoutSeconds { get; set; } = 15;
}
