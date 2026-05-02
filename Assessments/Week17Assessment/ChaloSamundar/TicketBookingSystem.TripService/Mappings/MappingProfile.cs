using AutoMapper;
using TicketBookingSystem.TripService.DTOs;
using TicketBookingSystem.TripService.Models;

namespace TicketBookingSystem.TripService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateTripDto → Trip (POST flow)
            CreateMap<CreateTripDto, Trip>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())   // Id is DB-assigned
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true)); // default to active

            // Trip → TripWithItineraryDto (GET flow)
            CreateMap<Trip, TripWithItineraryDto>();
        }
    }
}
