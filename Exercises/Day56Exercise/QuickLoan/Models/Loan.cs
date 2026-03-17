using System.ComponentModel.DataAnnotations;

namespace QuickLoan.Models;

public class Loan
{
    public static int NextId { get; private set; } = 0;
    public int Id { get; set; }

    [Required]
    [Display(Name = "Borrower Name")]
    public required string BorrowerName { get; set; }

    [Required]
    [Display(Name = "Lender Name")]
    public required string LenderName { get; set; }

    [Range(1, 5_00_000)]
    public double Amount { get; set; }

    [Display(Name = "Settlement Status")]
    public bool IsSettled { get; set; }

    public Loan () { NextId += 1; }
}