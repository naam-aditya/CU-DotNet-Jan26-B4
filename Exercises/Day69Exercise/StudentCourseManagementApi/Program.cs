using Microsoft.EntityFrameworkCore;
using StudentCourseManagementApi.Data;

namespace StudentCourseManagementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext"))
            );

            builder.Services.AddHttpsRedirection(
                options => options.HttpsPort = 7129
            );
            builder.WebHost.ConfigureKestrel(
                options =>
                {
                    options.ListenLocalhost(5001);
                    options.ListenLocalhost(7129, listenOptions => { listenOptions.UseHttps(); });
                }
            );

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}