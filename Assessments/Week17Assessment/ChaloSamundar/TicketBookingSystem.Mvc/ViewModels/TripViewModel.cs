using System;

namespace TicketBookingSystem.Mvc.ViewModels;

public class TripViewModel
{
    // public int Id { get; set; }
    // public string? Heading { get; set; }
    // public string? TripType { get; set; }
    // public string[] Ports { get; set; } = [];
    // public int Nights { get; set; }
    // public int Days { get; set; }
    // public DateTime Embarkation { get; set; }
    // public DateTime Disembarkation { get; set; }
    // public int Price { get; set; }
    public int Id { get; set; }
    public string? Heading { get; set; }
    public string? ShipName { get; set; }
    public string? TripType { get; set; }
    public string[] Ports { get; set; } = [];
    public int Nights { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }

    public List<ItineraryItemViewModel> Itinerary { get; set; } = new();
}
