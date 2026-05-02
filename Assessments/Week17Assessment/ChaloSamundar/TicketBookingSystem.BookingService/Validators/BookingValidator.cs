using FluentValidation;
using TicketBookingSystem.BookingService.DTOs;

namespace TicketBookingSystem.BookingService.Validators;

public class BookingValidator : AbstractValidator<BookingRequestDto>
{
    public BookingValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        
        RuleFor(x => x.BookingReference)
            .NotEmpty().WithMessage("BookingReference is required.");
        
        RuleFor(x => x.SourcePort)
            .NotEmpty().WithMessage("SourcePort is required.");

        RuleFor(x => x.SourcePortCode)
            .NotEmpty().WithMessage("SourcePortCode is required.");

        RuleFor(x => x.DestinationPort)
            .NotEmpty().WithMessage("DestinationPort is required.");
        
        RuleFor(x => x.DestinationPortCode)
            .NotEmpty().WithMessage("DestinationPortCode is required.");
        
        RuleFor(x => x.JourneyName)
            .NotEmpty().WithMessage("JourneyName is required.");
        
        RuleFor(x => x.CabinType)
            .NotEmpty().WithMessage("CabinType is required.");
        
        RuleFor(x => x.Embarkation)
            .GreaterThan(DateTime.Now)
            .WithMessage("Embarkation date must be in the future.");
        
        RuleFor(x => x.Disembarkation)
            .GreaterThan(x => x.Embarkation)
            .WithMessage("Disembarkation date must be after Embarkation date.");
        
        RuleFor(x => x.BookingDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("BookingDate cannot be in the future.");

        RuleFor(x => x.Nights)
            .GreaterThan(0).WithMessage("Nights must be greater than 0.");
        
        RuleFor(x => x.BaseFare)
            .GreaterThan(0).WithMessage("BaseFare must be greater than 0.");
        
        RuleFor(x => x.ConvenienceFee)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ConvenienceFee cannot be negative.");
        
        RuleFor(x => x.TotalFare)
            .GreaterThan(0)
            .WithMessage("TotalFare must be greater than 0.")
            .GreaterThanOrEqualTo(x => x.BaseFare + x.ConvenienceFee)
            .WithMessage("TotalFare must be greater than or equal to the sum of BaseFare and ConvenienceFee.");
        
        RuleFor(x => x.Passengers)
            .NotEmpty().WithMessage("At least one passenger is required.")
            .ForEach(passenger =>
            { passenger.SetValidator(new PassengerValidator()); });
    }
}
