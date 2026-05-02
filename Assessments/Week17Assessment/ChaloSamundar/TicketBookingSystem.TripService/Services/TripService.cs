using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.TripService.DTOs;
using TicketBookingSystem.TripService.Exceptions;
using TicketBookingSystem.TripService.Models;
using TicketBookingSystem.TripService.Repositories;

namespace TicketBookingSystem.TripService.Services
{
    
    public class TripService : ITripService
    {
        private readonly ITripRepository _repository;
        private readonly HttpClient _itineraryhttp;
        private readonly IMapper _mapper;

        public TripService(ITripRepository repository, IHttpClientFactory clientFactory, IMapper mapper)
        {
            _repository = repository;
            _itineraryhttp = clientFactory.CreateClient("ItineraryService");
            _mapper = mapper;
        }

        public async Task<TripWithItineraryDto> GetTripByIdAsync(int id)
        {
            var trip = await _repository.GetByIdAsync(id);

            if (trip == null)
                throw new NotFoundException($"Trip with ID {id} not found");

            List<ItineraryItemDto> itinerary;

            try
            {
                itinerary = await _itineraryhttp
                    .GetFromJsonAsync<List<ItineraryItemDto>>($"api/itinerary/trip/{id}")
                    ?? new();
            }
            catch
            {
                itinerary = new(); // fallback if service fails
            }

            return new TripWithItineraryDto
            {
                Id = trip.Id,
                Heading = trip.Heading,
                ShipName = trip.ShipName,
                TripType = trip.TripType,
                Ports = trip.Ports,
                Nights = trip.Nights,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Price = trip.Price,
                ImageUrl = trip.ImageUrl,
                Itinerary = itinerary
            };
        }






        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _repository.GetAllAsync();
        }

        //public async Task<Trip> GetTripByIdAsync(int id)
        //{
        //    var trip = await _repository.GetByIdAsync(id);

        //    if (trip == null)
        //        throw new NotFoundException($"Trip with ID {id} not found");

        //    return trip;
        //}

        public async Task<Trip> CreateTripAsync(CreateTripDto dto)
        {
            var trip = _mapper.Map<Trip>(dto);
            await _repository.AddAsync(trip);
            return trip;
        }

        public async Task UpdateTripAsync(int id, Trip trip)
        {
            if (id != trip.Id)
                throw new BadRequestException("Trip ID mismatch");

            if (!await _repository.ExistsAsync(id))
                throw new NotFoundException($"Trip with ID {id} not found");

            try
            {
                await _repository.UpdateAsync(trip);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Concurrency error occurred while updating trip");
            }
        }

        public async Task DeleteTripAsync(int id)
        {
            var trip = await _repository.GetByIdAsync(id);

            if (trip == null)
                throw new NotFoundException($"Trip with ID {id} not found");

            await _repository.DeleteAsync(trip);
        }
    }
}