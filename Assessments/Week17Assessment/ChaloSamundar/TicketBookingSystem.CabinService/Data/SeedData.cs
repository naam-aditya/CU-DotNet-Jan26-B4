using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CabinDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<CabinDbContext>>());

        // Only seed if the database is empty
        if (context.CabinTypes.Any())
        {
            return;
        }

        // 1. Create Cabin Types based on UI Images
        var cabinTypes = new List<CabinType>
        {
            new CabinType { 
                Name = "Interior Upper", 
                Description = "Our cosy budget escape on middle decks.", 
                MaxOccupancy = 2, PricePerPerson = 26435, BaseOccupancy = 2, IsActive = true,
                Amenities = new List<Amenity> { 
                    new Amenity { Name = "Twin Beds" }, 
                    new Amenity { Name = "En-suite Bath" } 
                },
                Images = new List<CabinImage> { 
                    new CabinImage { ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945", DisplayOrder = 1 } 
                }
            },
            new CabinType { 
                Name = "Interior Premier", 
                Description = "Upgraded bedding and premium location.", 
                MaxOccupancy = 2, PricePerPerson = 26838, BaseOccupancy = 2, IsActive = true,
                Amenities = new List<Amenity> { 
                    new Amenity { Name = "LED TV" }, 
                    new Amenity { Name = "Mini-Safe" } 
                }
            },
            new CabinType { 
                Name = "Ocean View Standard", 
                Description = "Wake up to breathtaking ocean views.", 
                MaxOccupancy = 2, PricePerPerson = 29254, BaseOccupancy = 2, IsActive = true,
                Amenities = new List<Amenity> { 
                    new Amenity { Name = "Window View" }, 
                    new Amenity { Name = "Queen Bed" } 
                }
            },
            new CabinType { 
                Name = "Mini-Suite with Balcony", 
                Description = "Spacious living area with private outdoor space.", 
                MaxOccupancy = 4, PricePerPerson = 45000, BaseOccupancy = 2, IsActive = true,
                Amenities = new List<Amenity> { 
                    new Amenity { Name = "Private Balcony" }, 
                    new Amenity { Name = "Sofa Bed" },
                    new Amenity { Name = "Priority Check-in" }
                }
            },
            new CabinType { 
                Name = "Chairman's Suite", 
                Description = "The ultimate luxury experience with master bedroom.", 
                MaxOccupancy = 6, PricePerPerson = 134889, BaseOccupancy = 2, IsActive = true,
                Amenities = new List<Amenity> { 
                    new Amenity { Name = "Master Bedroom", IconUrl = "bed.png" }, 
                    new Amenity { Name = "Walk-in Closet", IconUrl = "closet.png" },
                    new Amenity { Name = "Jacuzzi", IconUrl = "hot-tub.png" },
                    new Amenity { Name = "Personal Butler" }
                }
            }
        };

        context.CabinTypes.AddRange(cabinTypes);
        await context.SaveChangesAsync();

        // 2. Generate 15+ Random Cabins across Ship IDs 1, 2, and 3
        var random = new Random();
        var cabins = new List<Cabin>();

        // We'll create at least 15 cabins
        for (int i = 0; i < 20; i++)
        {
            var typeIndex = random.Next(0, cabinTypes.Count);
            var shipId = random.Next(1, 4); // Ships 1, 2, or 3
            var deck = random.Next(1, 10);
            
            cabins.Add(new Cabin
            {
                CabinTypeId = cabinTypes[typeIndex].Id,
                ShipId = shipId,
                CabinNumber = $"{(char)random.Next(65, 75)}-{random.Next(100, 999)}", // e.g. A-123
                DeckNumber = deck,
                IsAvailable = i % 5 != 0 // Make most available, some booked
            });
        }

        context.Cabins.AddRange(cabins);
        await context.SaveChangesAsync();
    }
}
