using ai_analysis_service.Dtos;

namespace ai_analysis_service.Services;

public interface ISentimentAnalyzer
{
    AnalyzeFeedbackResponse Analyze(string text);
}
