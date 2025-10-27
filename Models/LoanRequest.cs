using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanTech.Models;

public class LoanRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ApplicantName { get; set; } = string.Empty;

    public string? AccountId { get; set; }

    [Column(TypeName = "REAL")]
    public decimal Amount { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
