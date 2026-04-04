using Microsoft.EntityFrameworkCore;
using Vagabond.TravelDestinationService.Data;
using Vagabond.TravelDestinationService.Exceptions;
using Vagabond.TravelDestinationService.Repositories;
using Vagabond.TravelDestinationService.Services;
namespace Vagabond.TravelDestinationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DestinationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DestinationDbContext") 
                    ?? throw new InvalidOperationException("Connection string 'DestinationDbContext' not found.")));

            // Add services to the container.

            builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
            builder.Services.AddScoped<IDestinationService, DestinationService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
