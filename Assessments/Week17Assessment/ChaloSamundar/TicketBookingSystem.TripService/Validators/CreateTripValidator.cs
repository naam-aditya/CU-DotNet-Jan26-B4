using FluentValidation;
using TicketBookingSystem.TripService.DTOs;

namespace TicketBookingSystem.TripService.Validators
{
    public class CreateTripValidator : AbstractValidator<CreateTripDto>
    {
        public CreateTripValidator()
        {
            RuleFor(x => x.Heading)
                .NotEmpty().WithMessage("Heading is required")
                .MaximumLength(150).WithMessage("Heading must be 150 characters or less");

            RuleFor(x => x.ShipName)
                .NotEmpty().WithMessage("Ship name is required")
                .MaximumLength(60);

            RuleFor(x => x.TripType)
                .NotEmpty().WithMessage("TripType is required")
                .Must(v => v == "RoundWay" || v == "OneWay")
                .WithMessage("TripType must be 'RoundWay' or 'OneWay'");

            RuleFor(x => x.Ports)
                .NotEmpty().WithMessage("At least one port is required")
                .Must(p => p != null && p.Length >= 2)
                .WithMessage("At least 2 ports are required");

            RuleFor(x => x.Nights)
                .GreaterThan(0).WithMessage("Nights must be greater than 0")
                .LessThanOrEqualTo(30).WithMessage("Nights must be 30 or fewer");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("StartDate must be before EndDate");

            RuleFor(x => x.ImageUrl)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ImageUrl must be a valid absolute URL");

            // Cross-field rule: RoundWay must start and end at same port; OneWay must not.
            RuleFor(x => x).Custom((dto, ctx) =>
            {
                if (dto.Ports == null || dto.Ports.Length < 2) return;
                var first = dto.Ports[0];
                var last = dto.Ports[^1];

                if (dto.TripType == "RoundWay" && first != last)
                    ctx.AddFailure(nameof(dto.Ports),
                        "RoundWay trips must start and end at the same port");

                if (dto.TripType == "OneWay" && first == last)
                    ctx.AddFailure(nameof(dto.Ports),
                        "OneWay trips must have different start and end ports");
            });

            // Nights must equal day count between dates
            RuleFor(x => x).Custom((dto, ctx) =>
            {
                var dayCount = (dto.EndDate.Date - dto.StartDate.Date).Days;
                if (dayCount > 0 && dayCount != dto.Nights)
                    ctx.AddFailure(nameof(dto.Nights),
                        $"Nights ({dto.Nights}) must equal day count between dates ({dayCount})");
            });
        }
    }
}
