using AutoMapper;
using TicketBookingSystem.BookingService.DTOs;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingRequestDto, Booking>()
            .ForMember(dest => dest.NumberOfPassengers, 
                opt => opt.MapFrom(src => src.Passengers.Count));
            
        //CreateMap<Booking, BookingResponseDto>()
            //.ForMember(dest => dest.Passengers, opt => opt.Ignore());
        CreateMap<Booking, BookingResponseDto>()
    .ForMember(dest => dest.Passengers, opt => opt.Ignore())
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)Enum.Parse(typeof(BookingStatus), src.Status)));
    }
}
