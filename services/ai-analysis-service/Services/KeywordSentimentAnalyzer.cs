using System.Text.RegularExpressions;
using ai_analysis_service.Dtos;

namespace ai_analysis_service.Services;

public partial class KeywordSentimentAnalyzer : ISentimentAnalyzer
{
    private static readonly HashSet<string> NegativeWords =
    [
        "slow", "delay", "delayed", "bad", "poor", "issue", "problem", "broken", "error", "frustrated", "late"
    ];

    private static readonly HashSet<string> PositiveWords =
    [
        "great", "good", "excellent", "fast", "helpful", "smooth", "resolved", "amazing", "quick", "nice"
    ];

    private static readonly HashSet<string> StopWords =
    [
        "the", "a", "an", "and", "or", "to", "of", "for", "is", "was", "it", "this", "that", "with", "on", "in"
    ];

    public AnalyzeFeedbackResponse Analyze(string text)
    {
        var words = WordRegex()
            .Matches(text.ToLowerInvariant())
            .Select(m => m.Value)
            .Where(w => w.Length > 2)
            .ToList();

        var positive = words.Count(w => PositiveWords.Contains(w));
        var negative = words.Count(w => NegativeWords.Contains(w));

        var sentiment = negative > positive
            ? "Negative"
            : positive > negative
                ? "Positive"
                : "Neutral";

        var keywords = words
            .Where(w => !StopWords.Contains(w))
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        return new AnalyzeFeedbackResponse
        {
            Sentiment = sentiment,
            Keywords = keywords
        };
    }

    [GeneratedRegex(@"[a-zA-Z]+", RegexOptions.Compiled)]
    private static partial Regex WordRegex();
}
