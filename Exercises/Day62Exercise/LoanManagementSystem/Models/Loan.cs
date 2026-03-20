using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public required string BorrowerName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int LoanTermInMonths { get; set; }

        public bool IsApproved { get; set; }
    }
}