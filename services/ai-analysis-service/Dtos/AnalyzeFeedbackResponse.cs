namespace ai_analysis_service.Dtos;

public class AnalyzeFeedbackResponse
{
    public required string Sentiment { get; set; }
    public List<string> Keywords { get; set; } = [];
}
