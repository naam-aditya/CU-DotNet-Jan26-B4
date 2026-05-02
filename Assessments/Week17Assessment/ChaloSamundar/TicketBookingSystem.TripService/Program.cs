using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.TripService.Data;
using TicketBookingSystem.TripService.Mappings;
using TicketBookingSystem.TripService.Repositories;
using TicketBookingSystem.TripService.Services;
using TicketBookingSystem.TripService.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TripDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TripDbContext")
            ?? throw new InvalidOperationException("Connection string 'TripDbContext' not found."),
        sql => sql.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

// AutoMapper — scans the assembly for Profile classes (MappingProfile)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation — register every validator in the assembly + auto-validation hook
builder.Services.AddValidatorsFromAssemblyContaining<CreateTripValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddHttpClient("ItineraryService", c =>
{
    c.BaseAddress = new Uri("https://localhost:7004/"); // your itinerary service port
});

// JWT configuration

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(
//         options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                 ValidAudience = builder.Configuration["Jwt:Audience"],
//                 IssuerSigningKey = new SymmetricSecurityKey(
//                     Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
//                 )
//             };
//         }
//     );

// Add services to the container.

// builder.Services.AddAuthorization();

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Map NotFoundException → 404, BadRequestException → 400 instead of raw 500.
app.UseMiddleware<TicketBookingSystem.TripService.Exceptions.GlobalExceptionMiddleware>();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
