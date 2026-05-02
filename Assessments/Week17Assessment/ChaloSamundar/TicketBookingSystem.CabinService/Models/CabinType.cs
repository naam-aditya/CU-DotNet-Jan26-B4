namespace TicketBookingSystem.CabinService.Models;

public class CabinType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxOccupancy { get; set; }
    public decimal PricePerPerson { get; set; }
    public int BaseOccupancy { get; set; } = 2;
    public bool IsActive { get; set; }
    //public int TotalCabins { get; set; }

    // Navigation Properties
    public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    public ICollection<CabinImage> Images { get; set; } = new List<CabinImage>();
    public ICollection<Cabin> Cabins { get; set; } = new List<Cabin>();
}
