using System.Net;

namespace TicketBookingSystem.BookingService.DTOs;

public class BookingApiResponse<T>
{
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public DateTime TimeStamp { get; set; }
}
