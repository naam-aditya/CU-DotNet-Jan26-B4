using System.ComponentModel.DataAnnotations;

namespace FinTrackPro.Models;

public class Account
{
    public int Id { get; set; }
    [Required]
    public string AccountHolderName { get; set; } = string.Empty;
    [Required]
    public double Balance { get; set; }
    public List<Transaction>? Transactions { get; set; }
}