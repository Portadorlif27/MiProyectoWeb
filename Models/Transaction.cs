using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanTech.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public string AccountId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    [Column(TypeName = "REAL")]
    public decimal Amount { get; set; }
    public string Type { get; set; } = "Payment";
}
