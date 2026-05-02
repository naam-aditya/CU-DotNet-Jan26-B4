using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Mvc.ViewModels
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        // True once the user has saved at least once — switches view to read-only mode
        public bool IsSaved { get; set; } = false;

        public int? Age => DateOfBirth.HasValue
            ? (int)((DateTime.Today - DateOfBirth.Value).TotalDays / 365.25)
            : null;
    }
}
