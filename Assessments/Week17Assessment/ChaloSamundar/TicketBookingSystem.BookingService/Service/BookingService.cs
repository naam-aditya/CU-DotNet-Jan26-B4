using TicketBookingSystem.BookingService.DTOs;
using TicketBookingSystem.BookingService.Models;
using TicketBookingSystem.BookingService.Repository;
using TicketBookingSystem.BookingService.Exceptions;
using AutoMapper;
using System.Net;

namespace TicketBookingSystem.BookingService.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository repository, IPassengerRepository passengerRepository, IMapper mapper)
        {
            _bookingRepository = repository;
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }

        public async Task<BookingResponseDto> CreateBooking(BookingRequestDto bookingRequest)
        {
            var booking = _mapper.Map<Booking>(bookingRequest);
            await _bookingRepository.CreateBooking(booking);

            var passengers = _mapper.Map<List<Passenger>>(bookingRequest.Passengers);
            passengers.ForEach(p => p.BookingReference = booking.BookingReference);
            await _passengerRepository.AddPassengersAsync(passengers);

            var response = _mapper.Map<BookingResponseDto>(booking);
            response.Passengers = bookingRequest.Passengers;

            return response;
        }

        public async Task<BookingApiResponse<BookingResponseDto>> GetBookingByReference(string bookingReference)
        {
            var booking = await _bookingRepository.GetBookingByReference(bookingReference) 
                    ?? throw new NotFoundException("Booking not found with the provided reference.");
            
            var passengers = await _passengerRepository.GetPassengersByBookingReferenceAsync(bookingReference);

            var response = _mapper.Map<BookingResponseDto>(booking);
            response.Passengers = _mapper.Map<List<PassengerDto>>(passengers);

            return new BookingApiResponse<BookingResponseDto>
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Booking details successfully fetched.",
                Data = response,
                TimeStamp = DateTime.UtcNow
            };
        }
        public async Task<BookingApiResponse<List<BookingResponseDto>>> GetBookingsByUserIdAsync(string userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);

            var responses = new List<BookingResponseDto>();
            foreach (var booking in bookings)
            {
                var passengers = await _passengerRepository.GetPassengersByBookingReferenceAsync(booking.BookingReference);
                var response = _mapper.Map<BookingResponseDto>(booking);
                response.Passengers = _mapper.Map<List<PassengerDto>>(passengers);
                responses.Add(response);
            }

            return new BookingApiResponse<List<BookingResponseDto>>
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Bookings fetched successfully.",
                Data = responses,
                TimeStamp = DateTime.UtcNow
            };
        }
        public async Task<BookingApiResponse<List<BookingResponseDto>>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            var responses = new List<BookingResponseDto>();
            foreach (var booking in bookings)
            {
                var passengers = await _passengerRepository
                    .GetPassengersByBookingReferenceAsync(booking.BookingReference);
                var response = _mapper.Map<BookingResponseDto>(booking);
                response.Passengers = _mapper.Map<List<PassengerDto>>(passengers);
                responses.Add(response);
            }
            return new BookingApiResponse<List<BookingResponseDto>>
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "All bookings fetched.",
                Data = responses,
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}
