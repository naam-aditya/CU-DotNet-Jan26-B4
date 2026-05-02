using AutoMapper;
using TicketBookingSystem.BookingService.DTOs;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Mappers;

public class PassengerProfile : Profile
{
    public PassengerProfile()
    {
        CreateMap<Passenger, PassengerDto>().ReverseMap();
    }
}
