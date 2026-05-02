using FluentValidation;
using TicketBookingSystem.BookingService.DTOs;

namespace TicketBookingSystem.BookingService.Validators;

public class PassengerValidator : AbstractValidator<PassengerDto>
{
    public PassengerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Passenger name is required.");
        
        RuleFor(x => x.Age)
            .GreaterThan(0).WithMessage("Passenger age must be greater than 0.");
        
        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Passenger gender is required.")
            .Must(g => 
                    g.Equals("Male", StringComparison.OrdinalIgnoreCase) ||
                    g.Equals("Female", StringComparison.OrdinalIgnoreCase) ||
                    g.Equals("Other", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Passenger gender must be 'Male', 'Female', or 'Other'.");
    }
}
