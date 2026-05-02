namespace TicketBookingSystem.Mvc.ViewModels;

public class CabinTypeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxOccupancy { get; set; }
    public decimal PricePerPerson { get; set; }
    public int BaseOccupancy { get; set; }
    
    public List<AmenityViewModel> Amenities { get; set; } = new();
    public List<CabinImageViewModel> Images { get; set; } = new();
}

public class AmenityViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }
}

public class CabinImageViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int DisplayOrder { get; set; }
}

public class CabinSelectionViewModel
{
    public int CruiseId { get; set; }
    public int ShipId { get; set; }
    public List<CabinTypeViewModel> CabinTypes { get; set; } = new();
}

