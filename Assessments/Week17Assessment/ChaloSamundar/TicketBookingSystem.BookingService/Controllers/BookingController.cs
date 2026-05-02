using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Net;
using TicketBookingSystem.BookingService.Data;
using TicketBookingSystem.BookingService.DTOs;
using TicketBookingSystem.BookingService.Service;

namespace TicketBookingSystem.BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        private readonly BookingDbContext _context;

        public BookingController(IBookingService service, BookingDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingRequestDto bookingReq)
        {
            var booking = await _service.CreateBooking(bookingReq);
            var response = new BookingApiResponse<BookingResponseDto>()
            {
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Message = "Booking created.",
                Data = booking,
                TimeStamp = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(CreateBooking), response);
        }

        // ── IMPORTANT: "user/{userId}" MUST be before "{reference}" ──
        // GET api/booking/user/someone@email.com
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _service.GetBookingsByUserIdAsync(userId);
            return Ok(result);
        }

        // GET api/booking/{reference}
        [HttpGet("{reference}")]
        public async Task<IActionResult> GetByBookingReference(string reference)
        {
            var booking = await _service.GetBookingByReference(reference);
            return Ok(booking);
        }
        // GET api/booking/all  (admin only)
        [HttpGet("all")]
        [Authorize(Roles = "Admin")] // ← replace with [Authorize(Roles="Admin")] once Admin JWT forwarding works
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _service.GetAllBookingsAsync();
            return Ok(result);
        }
    }
}
