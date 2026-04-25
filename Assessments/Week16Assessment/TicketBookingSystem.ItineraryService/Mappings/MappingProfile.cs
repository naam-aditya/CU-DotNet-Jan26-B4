using AutoMapper;
using TicketBookingSystem.ItineraryService.DTOs;
using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ItineraryItems mappings
            CreateMap<ItineraryItems, ItineraryItemDto>()
                .ForMember(dest => dest.FormattedDate, 
                    opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.Status, 
                    opt => opt.MapFrom(src => src.Date.Date == DateTime.UtcNow.Date ? "Today" : 
                                              src.Date.Date < DateTime.UtcNow.Date ? "Past" : "Upcoming"))
                .ForMember(dest => dest.DaysPassed, 
                    opt => opt.MapFrom(src => (int)(DateTime.UtcNow.Date - src.Date.Date).TotalDays));

            CreateMap<CreateItineraryItemDto, ItineraryItems>();
            CreateMap<UpdateItineraryItemDto, ItineraryItems>();
        }
    }
}
