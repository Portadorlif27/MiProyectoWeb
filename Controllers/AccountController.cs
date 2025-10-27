using Microsoft.AspNetCore.Mvc;
using FinanTech.Data;
using FinanTech.Models;

namespace FinanTech.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _db;
    public AccountController(AppDbContext db) => _db = db;

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var acc = _db.Accounts.FirstOrDefault(a => a.AccountId == id);
        if (acc == null) return NotFound(new { message = "Cuenta no encontrada" });
        return Ok(new { accountId = acc.AccountId, owner = acc.Owner, balance = acc.Balance });
    }

    public class PayRequest { public string AccountId { get; set; } = string.Empty; public decimal Amount { get; set; } }

    [HttpPost("pay")]
    public IActionResult Pay([FromBody] PayRequest req)
    {
        var acc = _db.Accounts.FirstOrDefault(a => a.AccountId == req.AccountId);
        if (acc == null) return NotFound(new { message = "Cuenta no encontrada" });
        if (req.Amount <= 0) return BadRequest(new { message = "Monto inválido" });
        if (acc.Balance < req.Amount) return BadRequest(new { message = "Fondos insuficientes" });

        acc.Balance -= req.Amount;
        _db.Transactions.Add(new Transaction { AccountId = acc.AccountId, Amount = -req.Amount, Type = "Payment" });
        _db.SaveChanges();

        // Notify via SignalR (optional)
        return Ok(new { message = "Pago procesado", newBalance = acc.Balance });
    }
}
