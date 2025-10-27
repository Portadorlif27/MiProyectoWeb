using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using FinanTech.Data;
using FinanTech.Hubs;
using FinanTech.Models;

namespace FinanTech.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IHubContext<NotificationHub> _hub;

    public CreditController(AppDbContext db, IHubContext<NotificationHub> hub)
    {
        _db = db;
        _hub = hub;
    }

    public class LoanDto
    {
        public string ApplicantName { get; set; } = string.Empty;
        public string? AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    [HttpPost("request")]
    public async Task<IActionResult> Request([FromBody] LoanDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.ApplicantName) || dto.Amount <= 0)
            return BadRequest(new { message = "Datos inválidos" });

        var loan = new LoanRequest
        {
            ApplicantName = dto.ApplicantName,
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            Reason = dto.Reason,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _db.LoanRequests.Add(loan);
        await _db.SaveChangesAsync();

        // notify
        await _hub.Clients.All.SendAsync("LoanRequested", new {
            id = loan.Id,
            applicantName = loan.ApplicantName,
            accountId = loan.AccountId,
            amount = loan.Amount,
            reason = loan.Reason,
            status = loan.Status,
            createdAt = loan.CreatedAt
        });

        return Ok(new { message = "Solicitud recibida", loanId = loan.Id });
    }

    [HttpGet("requests")]
    public IActionResult Requests()
    {
        var list = _db.LoanRequests.OrderByDescending(l => l.CreatedAt)
            .Select(l => new { l.Id, l.ApplicantName, l.AccountId, l.Amount, l.Reason, l.Status, l.CreatedAt })
            .ToList();
        return Ok(list);
    }
}
