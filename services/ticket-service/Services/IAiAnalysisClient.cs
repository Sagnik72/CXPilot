namespace ticket_service.Services;

public interface IAiAnalysisClient
{
    Task<AiAnalyzeResponse> AnalyzeFeedbackAsync(string feedbackText, CancellationToken ct);
}
