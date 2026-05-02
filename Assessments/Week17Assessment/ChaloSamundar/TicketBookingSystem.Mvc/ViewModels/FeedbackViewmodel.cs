using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketBookingSystem.Mvc.ViewModels;

public class FeedbackViewmodel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BindNever]
    public string? Id { get; set; }
    [Required]
    public string BookingId { get; set; } = null!;
    [Required]
    public string TripId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Message { get; set; } = null!;
    [Range(1, 5)]
    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
