using System.ComponentModel.DataAnnotations;

namespace FinTrackPro.Models;

public class Transaction
{
    public int Id { get; set; }
    [Required]
    public required Account Account { get; set; }
    public string Description { get; set; } = string.Empty;
    [Required]
    public double Amount { get; set; }
    [Required]
    public string Category { get; set; } = string.Empty;
    [Required]
    public DateTime Date { get; set; }
}