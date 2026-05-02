using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.BookingService.Data;
using TicketBookingSystem.BookingService.Helpers;
using TicketBookingSystem.BookingService.Repository;
using TicketBookingSystem.BookingService.Service;
using TicketBookingSystem.BookingService.Exceptions;
using TicketBookingSystem.BookingService.HttpClients;
using TicketBookingSystem.BookingService.Mappers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Define the JWT bearer scheme so Swagger UI shows an "Authorize" button.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter: Bearer <your JWT token>",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Apply the scheme globally so every endpoint requires the token in Swagger UI.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<BookingDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new NotFoundException("Connection string not found")));

// ======================= Repositories ========================
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();

// ======================= Services ========================
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddHttpClient<ICabinServiceClient, CabinServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CabinService"] 
        ?? throw new NotFoundException("CabinService URL not found"));
});

// ============ Configuration Setup ============
var jwtSection = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSection["Key"] 
    ?? throw new InvalidOperationException("JWT Key is not configured. Set Jwt:Key in appsettings.");

var jwtIssuer = jwtSection["Issuer"] 
    ?? throw new InvalidOperationException("JWT Issuer is not configured. Set Jwt:Issuer in appsettings.");

var jwtAudience = jwtSection["Audience"] 
    ?? throw new InvalidOperationException("JWT Audience is not configured. Set Jwt:Audience in appsettings.");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// ======================= Mappers ========================
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<BookingProfile>();
    options.AddProfile<PassengerProfile>();
});
builder.Services.AddScoped<ICabinAvailabilityRepository, CabinAvailabilityRepository>();
builder.Services.AddScoped<ICabinAvailabilityService, CabinAvailabilityService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
