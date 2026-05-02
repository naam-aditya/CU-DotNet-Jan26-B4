using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketBookingSystem.PaymentService.Data;
using TicketBookingSystem.PaymentService.Repositories;
using TicketBookingSystem.PaymentService.Services;
using TicketBookingSystem.QRCodeService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TicketBookingSystemPaymentServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketBookingSystemPaymentServiceContext") ?? throw new InvalidOperationException("Connection string 'TicketBookingSystemPaymentServiceContext' not found.")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentService, PaymentsService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IQRCodeService, QRCodesService>();
//builder.Services.AddHttpClient();
builder.Services.AddHttpClient("default", client => { })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors();

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
