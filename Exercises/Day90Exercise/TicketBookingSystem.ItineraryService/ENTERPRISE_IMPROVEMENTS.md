# Itinerary Service - Enterprise Architecture Implementation

## Overview
This document outlines all the enterprise-level improvements and patterns implemented in the TicketBookingSystem.ItineraryService.

---

## 1. **Repository Pattern with Generic Base**

### What's New
- **GenericRepository.cs** - Base repository with common CRUD operations
- **IItineraryRepository** - Specialized interface extending generic repository
- **ItineraryRepository** - Concrete implementation with domain-specific methods

### Key Benefits
- Separation of concerns between data access and business logic
- Reusable generic methods (GetById, GetAll, Add, Update, Delete, etc.)
- Specialized queries for itinerary operations
- Built-in transaction management with SaveChangesAsync()

### Available Methods
```csharp
// Generic methods
GetByIdAsync(int id)
GetAllAsync()
FindAsync(Expression<Func<T, bool>> predicate)
FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
AddAsync(T entity)
AddRangeAsync(IEnumerable<T> entities)
Update(T entity)
Remove(T entity)
RemoveRange(IEnumerable<T> entities)
SaveChangesAsync()

// Itinerary-specific methods
GetByTripIdAsync(int tripId)
GetByTripIdAndDayNumberAsync(int tripId, int dayNumber)
CountByTripIdAsync(int tripId)
ExistsByTripIdAndDayNumberAsync(int tripId, int dayNumber)
GetUniqueLocationsByTripIdAsync(int tripId)
GetAtSeaAndPortDaysCountAsync(int tripId)
```

---

## 2. **Fluent API (EF Core)**

### What's New
- **AppDbContext** updated with comprehensive Fluent API configuration
- Explicit entity mapping and constraint definitions

### Features Configured
✅ Primary key definition  
✅ Column names and data types  
✅ Max length constraints (Location: 200 chars)  
✅ Required field validation  
✅ Default values (IsAtSea: false)  
✅ Composite unique index (TripId + DayNumber)  
✅ Performance indexes on frequently queried columns  

### Example Usage
```csharp
entity.Property(e => e.Location)
    .IsRequired()
    .HasMaxLength(200);

entity.HasIndex(e => new { e.TripId, e.DayNumber })
    .HasDatabaseName("IX_ItineraryItems_TripId_DayNumber")
    .IsUnique();
```

---

## 3. **FluentValidation**

### What's New
- **ItineraryValidators.cs** - Validation rules for create and update operations
- Two validators: `CreateItineraryItemValidator` and `UpdateItineraryItemValidator`

### Validation Rules
| Field | Rules |
|-------|-------|
| TripId | Must be > 0 |
| DayNumber | Between 1-365, required |
| Location | 2-200 characters, required |
| Date | Must be today or future |
| IsAtSea | Required, non-null |

### Benefits
✅ Declarative, reusable validation rules  
✅ Automatic model state validation in controller  
✅ Detailed error messages  
✅ Fluent syntax for easy reading/maintenance  

### Integration in Controller
```csharp
var validationResult = await _createValidator.ValidateAsync(dto);
if (!validationResult.IsValid)
{
    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
    return BadRequest(ApiResponse<ItineraryItemDto>.FailureResponse(
        "Validation failed.", 400, errors));
}
```

---

## 4. **Service Layer with Business Logic**

### What's New
- **IItineraryService** - Enhanced interface with complete CRUD + business operations
- **ItineraryServices** - Rich business logic implementation

### Core Responsibilities
✅ Input validation via FluentValidation  
✅ Duplicate detection before insert/update  
✅ Business rule enforcement (e.g., day number constraints)  
✅ DTO to Model mapping via AutoMapper  
✅ Structured error handling with custom exceptions  
✅ Structured logging with Serilog  
✅ Calculated fields computation (FormattedDate, Status, DaysPassed)  

### Available Operations
```csharp
GetByIdAsync(int id)
GetByTripIdAsync(int tripId)
GetTripSummaryAsync(int tripId) // Returns statistics
CreateAsync(CreateItineraryItemDto dto)
UpdateAsync(UpdateItineraryItemDto dto)
DeleteAsync(int id)
GetUpcomingAsync(int days = 7)
GetUniqueLocationsAsync(int tripId)
```

### Example: GetTripSummaryAsync
Returns comprehensive trip statistics:
```csharp
{
  "tripId": 1,
  "totalDays": 7,
  "atSeaDays": 4,
  "portDays": 3,
  "locations": ["Miami", "Cozumel", "Jamaica"],
  "items": [...]
}
```

---

## 5. **AutoMapper**

### What's New
- **MappingProfile.cs** - Centralized configuration for all entity-to-DTO mappings
- Two-way mappings: Model ↔ DTO

### Configured Mappings
```csharp
ItineraryItems ↔ ItineraryItemDto
CreateItineraryItemDto → ItineraryItems
UpdateItineraryItemDto → ItineraryItems
```

### Calculated Fields (Computed during mapping)
```csharp
FormattedDate: src.Date.ToString("yyyy-MM-dd HH:mm:ss")
Status: "Today" | "Past" | "Upcoming" (based on current date)
DaysPassed: (int)(DateTime.UtcNow.Date - src.Date.Date).TotalDays
```

### Benefits
✅ Centralized mapping logic  
✅ No manual property-to-property assignment  
✅ Supports calculated/derived fields  
✅ Easy to test and maintain  

---

## 6. **Exception Middleware**

### What's New
- **ExceptionHandlingMiddleware.cs** - Global exception handler
- **ItineraryException.cs** - Custom exception hierarchy

### Exception Types
| Exception | Status Code | Purpose |
|-----------|------------|---------|
| `ItineraryNotFoundException` | 404 | Item not found |
| `InvalidItineraryException` | 400 | Invalid data |
| `DuplicateItineraryException` | 409 | Duplicate entry |
| `TripNotFoundException` | 404 | Trip not found |
| General Exception | 500 | Unexpected errors |

### Middleware Features
✅ Catches all unhandled exceptions  
✅ Logs exceptions with context  
✅ Returns standardized error responses  
✅ Handles FluentValidation exceptions separately  
✅ Maps custom exceptions to appropriate HTTP status codes  

### Example Response
```json
{
  "success": false,
  "message": "Validation failed.",
  "statusCode": 400,
  "errors": ["Location is required.", "DayNumber must be greater than 0"],
  "timestamp": "2026-04-25T10:30:00.0000000Z"
}
```

---

## 7. **Serilog**

### What's New
- **Program.cs** configured with Serilog for structured logging
- Logging integrated throughout Service and Controller layers

### Configuration
- Minimum level: Information
- Output: Console
- Format: `[Timestamp {Level}] {Message} {Exception}`
- Enriched with LogContext

### Logging Points
✅ Information logs for successful operations  
✅ Warning logs for business rule violations  
✅ Error logs for exceptions  
✅ Includes parameters (IDs, counts, etc.) in log context  

### Example Logs
```
[2026-04-25 10:30:00 INF] Fetching itinerary item with ID: 1
[2026-04-25 10:30:01 WRN] Duplicate itinerary entry attempted for Trip ID: 5, Day: 3
[2026-04-25 10:30:02 ERR] Error creating itinerary item
```

---

## 8. **Standard API Response Wrapper**

### What's New
- **ApiResponse.cs** - Generic response wrapper for all endpoints
- Two variants: Generic `ApiResponse<T>` and non-generic `ApiResponse`

### Response Structure
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public int? StatusCode { get; set; }
    public List<string>? Errors { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### Helper Methods
```csharp
// Success response
ApiResponse<T>.SuccessResponse(data, message, statusCode)

// Failure response
ApiResponse<T>.FailureResponse(message, statusCode, errors)
```

### Example Success Response
```json
{
  "success": true,
  "message": "Itinerary item created successfully.",
  "data": {
    "id": 1,
    "tripId": 5,
    "dayNumber": 1,
    "location": "Miami",
    "date": "2026-05-01T08:00:00",
    "isAtSea": false,
    "formattedDate": "2026-05-01 08:00:00",
    "status": "Upcoming",
    "daysPassed": -6
  },
  "statusCode": 201,
  "timestamp": "2026-04-25T10:30:00.0000000Z"
}
```

### Example Error Response
```json
{
  "success": false,
  "message": "Validation failed.",
  "statusCode": 400,
  "errors": ["Location cannot exceed 200 characters"],
  "timestamp": "2026-04-25T10:30:00.0000000Z"
}
```

---

## 9. **DTOs (Data Transfer Objects)**

### What's New
- **ItineraryDtos.cs** - Separate DTOs for different operations

### DTO Types

#### CreateItineraryItemDto
Used for POST requests to create new items.
```csharp
public int TripId { get; set; }
public int DayNumber { get; set; }
public string Location { get; set; }
public DateTime Date { get; set; }
public bool IsAtSea { get; set; }
```

#### UpdateItineraryItemDto
Used for PUT requests to update existing items.
```csharp
public int Id { get; set; }
public int TripId { get; set; }
public int DayNumber { get; set; }
public string Location { get; set; }
public DateTime Date { get; set; }
public bool IsAtSea { get; set; }
```

#### ItineraryItemDto
Response DTO with calculated fields.
```csharp
public int Id { get; set; }
public int TripId { get; set; }
public int DayNumber { get; set; }
public string Location { get; set; }
public DateTime Date { get; set; }
public bool IsAtSea { get; set; }
public string FormattedDate { get; set; } // Calculated
public string Status { get; set; } // Calculated
public int DaysPassed { get; set; } // Calculated
```

#### ItineraryTripSummaryDto
Response DTO with trip statistics.
```csharp
public int TripId { get; set; }
public int TotalDays { get; set; }
public int AtSeaDays { get; set; }
public int PortDays { get; set; }
public List<string> Locations { get; set; }
public List<ItineraryItemDto> Items { get; set; }
```

---

## 10. **API Endpoints**

### Updated Controller Methods

#### GET: Retrieve Single Item
```http
GET /api/itinerary/{id}
Response: ApiResponse<ItineraryItemDto>
```

#### GET: List by Trip
```http
GET /api/itinerary/trip/{tripId}
Response: ApiResponse<List<ItineraryItemDto>>
```

#### GET: Trip Summary
```http
GET /api/itinerary/trip/{tripId}/summary
Response: ApiResponse<ItineraryTripSummaryDto>
```

#### POST: Create Item
```http
POST /api/itinerary
Body: CreateItineraryItemDto
Response: ApiResponse<ItineraryItemDto> (Status: 201)
```

#### PUT: Update Item
```http
PUT /api/itinerary/{id}
Body: UpdateItineraryItemDto
Response: ApiResponse<ItineraryItemDto>
```

#### DELETE: Remove Item
```http
DELETE /api/itinerary/{id}
Response: ApiResponse
```

#### GET: Upcoming Items
```http
GET /api/itinerary/upcoming?days=7
Response: ApiResponse<List<ItineraryItemDto>>
```

#### GET: Unique Locations
```http
GET /api/itinerary/trip/{tripId}/locations
Response: ApiResponse<List<string>>
```

---

## 11. **Project Structure**

```
TicketBookingSystem.ItineraryService/
├── Common/
│   └── ApiResponse.cs                 # Response wrapper
├── Controllers/
│   └── ItineraryController.cs         # API endpoints (updated)
├── Data/
│   └── AppDbContext.cs                # DbContext with Fluent API
├── DTOs/
│   └── ItineraryDtos.cs               # Request/Response DTOs
├── Exceptions/
│   └── ItineraryException.cs          # Custom exceptions
├── Mappings/
│   └── MappingProfile.cs              # AutoMapper configuration
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs # Global exception handler
├── Models/
│   └── ItineraryItems.cs              # Entity model
├── Repositories/
│   ├── GenericRepository.cs           # Generic CRUD base
│   ├── IItineraryRepository.cs        # Domain repository interface
│   └── ItineraryRepository.cs         # Domain repository implementation
├── Services/
│   ├── IItineraryService.cs           # Service interface (updated)
│   └── ItineraryServices.cs           # Service implementation (updated)
├── Validators/
│   └── ItineraryValidators.cs         # FluentValidation validators
├── Program.cs                         # Configuration (updated)
└── TicketBookingSystem.ItineraryService.csproj  # Project file (updated)
```

---

## 12. **Package Dependencies Added**

```xml
<PackageReference Include="FluentValidation" Version="11.9.1" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.1" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="Serilog" Version="3.1.1" />
<PackageReference Include="Serilog.AspNetCore" Version="8.0.1" />
<PackageReference Include="Serilog.Sinks.Console" Version="5.0.1" />
```

---

## 13. **How to Use the Service**

### Example 1: Create an Itinerary Item
```csharp
POST /api/itinerary
Content-Type: application/json

{
  "tripId": 5,
  "dayNumber": 1,
  "location": "Miami",
  "date": "2026-05-01T08:00:00Z",
  "isAtSea": false
}
```

Response:
```json
{
  "success": true,
  "message": "Itinerary item created successfully.",
  "data": {
    "id": 1,
    "tripId": 5,
    "dayNumber": 1,
    "location": "Miami",
    "date": "2026-05-01T08:00:00",
    "isAtSea": false,
    "formattedDate": "2026-05-01 08:00:00",
    "status": "Upcoming",
    "daysPassed": -6
  },
  "statusCode": 201
}
```

### Example 2: Get Trip Summary
```http
GET /api/itinerary/trip/5/summary
```

Response:
```json
{
  "success": true,
  "message": "Trip summary retrieved successfully.",
  "data": {
    "tripId": 5,
    "totalDays": 7,
    "atSeaDays": 4,
    "portDays": 3,
    "locations": ["Miami", "Cozumel", "Jamaica", "Grand Cayman"],
    "items": [...]
  },
  "statusCode": 200
}
```

### Example 3: Handle Validation Error
```csharp
POST /api/itinerary
Content-Type: application/json

{
  "tripId": 5,
  "dayNumber": -1,    // Invalid: must be > 0
  "location": "X",    // Invalid: too short
  "date": "2025-01-01T08:00:00Z",  // Invalid: past date
  "isAtSea": false
}
```

Response:
```json
{
  "success": false,
  "message": "Validation failed.",
  "errors": [
    "DayNumber: Day number must be greater than 0.",
    "Location: Location must be at least 2 characters long.",
    "Date: Date must be today or in the future."
  ],
  "statusCode": 400
}
```

---

## 14. **Benefits Summary**

| Pattern | Benefits |
|---------|----------|
| **Repository Pattern** | Loose coupling, testability, centralized data access |
| **Fluent API** | Type-safe configuration, better performance, explicit constraints |
| **FluentValidation** | Reusable, declarative, easy to test |
| **Service Layer** | Business logic separation, code reuse, maintainability |
| **AutoMapper** | Reduces boilerplate, supports complex mapping |
| **Exception Middleware** | Centralized error handling, consistent responses |
| **Serilog** | Structured logging, easy debugging, production-ready |
| **Response Wrapper** | Consistent API contracts, better client integration |

---

## 15. **Next Steps / Future Enhancements**

1. **Add Caching** - Cache frequently accessed trips
2. **Add Pagination** - Paginate large result sets
3. **Add Authentication** - Secure endpoints with JWT
4. **Add Unit Tests** - Create comprehensive test suite
5. **Add API Documentation** - Enhance Swagger with examples
6. **Add Soft Deletes** - Maintain audit trail
7. **Add Audit Logging** - Track who changed what and when
8. **Add Rate Limiting** - Prevent abuse

---

## Questions & Support

For questions about any of the implemented patterns, refer to the inline XML comments in the code or the respective files mentioned in this guide.
