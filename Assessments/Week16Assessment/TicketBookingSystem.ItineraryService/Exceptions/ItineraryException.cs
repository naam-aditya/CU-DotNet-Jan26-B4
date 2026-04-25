namespace TicketBookingSystem.ItineraryService.Exceptions
{
    public class ItineraryException : Exception
    {
        public int StatusCode { get; set; } = 400;

        public ItineraryException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class ItineraryNotFoundException : ItineraryException
    {
        public ItineraryNotFoundException(string message = "Itinerary not found.") 
            : base(message, 404) { }
    }

    public class InvalidItineraryException : ItineraryException
    {
        public InvalidItineraryException(string message = "Invalid itinerary data.") 
            : base(message, 400) { }
    }

    public class DuplicateItineraryException : ItineraryException
    {
        public DuplicateItineraryException(string message = "Itinerary already exists.") 
            : base(message, 409) { }
    }

    public class TripNotFoundException : ItineraryException
    {
        public TripNotFoundException(string message = "Trip not found.") 
            : base(message, 404) { }
    }
}
