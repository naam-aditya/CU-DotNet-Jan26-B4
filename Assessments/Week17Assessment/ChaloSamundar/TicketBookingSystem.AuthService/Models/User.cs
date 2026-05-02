using Microsoft.AspNetCore.Identity;

namespace TicketBookingSystem.AuthService.Models;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
}
