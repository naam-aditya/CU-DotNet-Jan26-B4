using FluentValidation;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Validators;

public class BookingViewModelValidator : AbstractValidator<BookingViewModel>
{
    public BookingViewModelValidator()
    {
        RuleFor(x => x.BookingReference)
            .NotEmpty().WithMessage("Booking reference is required.");
    }
}
