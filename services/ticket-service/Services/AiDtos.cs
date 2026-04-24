namespace ticket_service.Services;

public class AiAnalyzeRequest
{
    public required string Text { get; set; }
}

public class AiAnalyzeResponse
{
    public required string Sentiment { get; set; }
    public List<string> Keywords { get; set; } = [];
}
