using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.CabinService.Data;
using TicketBookingSystem.CabinService.Repository;
using TicketBookingSystem.CabinService.Service;
using TicketBookingSystem.CabinService.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework
builder.Services.AddDbContext<CabinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// Register Repositories
builder.Services.AddScoped<ICabinRepository, CabinRepository>();
builder.Services.AddScoped<ICabinTypeRepository, CabinTypeRepository>();

// Register Services
builder.Services.AddScoped<ICabinService, TicketBookingSystem.CabinService.Service.CabinService>();
builder.Services.AddScoped<ICabinTypeService, CabinTypeService>();

var app = builder.Build();

// Migrate Database (Ensure structure is created)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<CabinDbContext>();
        context.Database.Migrate();
        await SeedData.Initialize(services);
    }
    catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 1801 || ex.Number == 2714)
    {
        // 1801 — Database already exists.
        // 2714 — Object (table) already exists.
        // Both happen on Azure SQL when the contained user can't read sys.* views,
        // so EF speculatively re-creates schema that's already in place. The DB is fine.
        logger.LogWarning("CabinService schema already present; skipping CREATE step. ({Number})", ex.Number);

        // Still seed in case data is missing.
        try { await SeedData.Initialize(services); }
        catch (Exception seedEx) { logger.LogWarning(seedEx, "SeedData skipped: {Message}", seedEx.Message); }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authorization / Authentication removed per user request for easier testing
app.MapControllers();

app.Run();
