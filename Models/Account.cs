using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanTech.Models;

public class Account
{
    [Key]
    public string AccountId { get; set; } = Guid.NewGuid().ToString();
    public string Owner { get; set; } = string.Empty;
    [Column(TypeName = "REAL")]
    public decimal Balance { get; set; }
    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
