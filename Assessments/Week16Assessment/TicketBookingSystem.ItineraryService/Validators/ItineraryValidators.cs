using FluentValidation;
using TicketBookingSystem.ItineraryService.DTOs;

namespace TicketBookingSystem.ItineraryService.Validators
{
    public class CreateItineraryItemValidator : AbstractValidator<CreateItineraryItemDto>
    {
        public CreateItineraryItemValidator()
        {
            RuleFor(x => x.TripId)
                .GreaterThan(0)
                .WithMessage("Trip ID must be greater than 0.");

            RuleFor(x => x.DayNumber)
                .GreaterThan(0)
                .WithMessage("Day number must be greater than 0.")
                .LessThanOrEqualTo(365)
                .WithMessage("Day number cannot exceed 365.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("Location is required.")
                .MaximumLength(200)
                .WithMessage("Location cannot exceed 200 characters.")
                .MinimumLength(2)
                .WithMessage("Location must be at least 2 characters long.");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be today or in the future.");

            RuleFor(x => x.IsAtSea)
                .NotNull()
                .WithMessage("IsAtSea flag is required.");
        }
    }

    public class UpdateItineraryItemValidator : AbstractValidator<UpdateItineraryItemDto>
    {
        public UpdateItineraryItemValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID must be greater than 0.");

            RuleFor(x => x.TripId)
                .GreaterThan(0)
                .WithMessage("Trip ID must be greater than 0.");

            RuleFor(x => x.DayNumber)
                .GreaterThan(0)
                .WithMessage("Day number must be greater than 0.")
                .LessThanOrEqualTo(365)
                .WithMessage("Day number cannot exceed 365.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("Location is required.")
                .MaximumLength(200)
                .WithMessage("Location cannot exceed 200 characters.")
                .MinimumLength(2)
                .WithMessage("Location must be at least 2 characters long.");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Date must be today or in the future.");

            RuleFor(x => x.IsAtSea)
                .NotNull()
                .WithMessage("IsAtSea flag is required.");
        }
    }
}
