using System.Net.Http.Json;
using ticket_service.Configurations;

namespace ticket_service.Services;

public class AiAnalysisClient(
    HttpClient httpClient,
    IConfiguration configuration,
    ILogger<AiAnalysisClient> logger) : IAiAnalysisClient
{
    public async Task<AiAnalyzeResponse> AnalyzeFeedbackAsync(string feedbackText, CancellationToken ct)
    {
        var path = configuration
            .GetSection(AiServiceOptions.SectionName)
            .GetValue<string>(nameof(AiServiceOptions.AnalyzeFeedbackPath))
            ?? "/api/analyze-feedback";

        var response = await httpClient.PostAsJsonAsync(
            path,
            new AiAnalyzeRequest { Text = feedbackText },
            ct);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            logger.LogWarning("AI service failed with status {Status}: {Body}", response.StatusCode, body);
            throw new HttpRequestException("AI service request failed.");
        }

        var result = await response.Content.ReadFromJsonAsync<AiAnalyzeResponse>(ct);
        if (result is null || string.IsNullOrWhiteSpace(result.Sentiment))
        {
            throw new InvalidOperationException("AI service returned invalid sentiment response.");
        }

        return result;
    }
}
