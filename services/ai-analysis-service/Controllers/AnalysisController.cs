using ai_analysis_service.Dtos;
using ai_analysis_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace ai_analysis_service.Controllers;

[ApiController]
[Route("api")]
public class AnalysisController(ISentimentAnalyzer sentimentAnalyzer) : ControllerBase
{
    [HttpPost("analyze-feedback")]
    public IActionResult AnalyzeFeedback([FromBody] AnalyzeFeedbackRequest request)
    {
        var response = sentimentAnalyzer.Analyze(request.Text.Trim());
        return Ok(response);
    }
}
