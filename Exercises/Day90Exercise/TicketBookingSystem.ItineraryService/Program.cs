
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketBookingSystem.ItineraryService.Data;
using TicketBookingSystem.ItineraryService.Mappings;
using TicketBookingSystem.ItineraryService.Middleware;
using TicketBookingSystem.ItineraryService.Repositories;
using TicketBookingSystem.ItineraryService.Services;
using TicketBookingSystem.ItineraryService.Validators;

namespace TicketBookingSystem.ItineraryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                Log.Information("Starting Itinerary Service application");
                var builder = WebApplication.CreateBuilder(args);

                // Add Serilog
                builder.Host.UseSerilog();

                // Add services to the container
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                // Register Repository
                builder.Services.AddScoped<IItineraryRepository, ItineraryRepository>();

                // Register Service Layer
                builder.Services.AddScoped<IItineraryService, ItineraryServices>();

                // Add FluentValidation
                builder.Services.AddValidatorsFromAssemblyContaining<CreateItineraryItemValidator>();
                builder.Services.AddFluentValidationAutoValidation();
                builder.Services.AddFluentValidationClientsideAdapters();

                // Add AutoMapper
                builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Itinerary Service API",
                        Version = "v1",
                        Description = "API for managing cruise trip itineraries"
                    });
                });

                var app = builder.Build();

                // Add Exception Handling Middleware
                app.UseMiddleware<ExceptionHandlingMiddleware>();

                // Configure the HTTP request pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information("Itinerary Service is running");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
