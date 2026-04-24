using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticket_service.Data;
using ticket_service.Dtos;
using ticket_service.Models;
using ticket_service.Services;

namespace ticket_service.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController(
    TicketDbContext dbContext,
    IAiAnalysisClient aiAnalysisClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var tickets = await dbContext.Tickets
            .OrderByDescending(t => t.CreatedAtUtc)
            .ToListAsync(ct);

        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var ticket = await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, ct);
        if (ticket is null)
        {
            return NotFound(new { message = "Ticket not found." });
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request, CancellationToken ct)
    {
        var ticket = new Ticket
        {
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Priority = request.Priority,
            GithubRepo = request.GithubRepo.Trim(),
            CreatedByUserId = request.CreatedByUserId,
            Status = TicketStatus.Open
        };

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketRequest request, CancellationToken ct)
    {
        var ticket = await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, ct);
        if (ticket is null)
        {
            return NotFound(new { message = "Ticket not found." });
        }

        ticket.Title = request.Title.Trim();
        ticket.Description = request.Description.Trim();
        ticket.Priority = request.Priority;
        ticket.Status = request.Status;
        ticket.GithubRepo = request.GithubRepo.Trim();
        ticket.AssignedToUserId = request.AssignedToUserId;
        ticket.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
        return Ok(ticket);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ticket = await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, ct);
        if (ticket is null)
        {
            return NotFound(new { message = "Ticket not found." });
        }

        dbContext.Tickets.Remove(ticket);
        await dbContext.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpPost("{id:guid}/feedback")]
    public async Task<IActionResult> SubmitFeedback(Guid id, [FromBody] SubmitFeedbackRequest request, CancellationToken ct)
    {
        var ticket = await dbContext.Tickets.SingleOrDefaultAsync(t => t.Id == id, ct);
        if (ticket is null)
        {
            return NotFound(new { message = "Ticket not found." });
        }

        if (ticket.Status != TicketStatus.Resolved)
        {
            return BadRequest(new { message = "Feedback can only be submitted for resolved tickets." });
        }

        var analysis = await aiAnalysisClient.AnalyzeFeedbackAsync(request.Message.Trim(), ct);

        return Ok(new
        {
            ticketId = ticket.Id,
            feedback = request.Message.Trim(),
            sentiment = analysis.Sentiment,
            keywords = analysis.Keywords
        });
    }
}
