using TicketBookingSystem.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<ICabinWebService, CabinWebService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CabinService"] ?? "https://localhost:7003");
});

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/");
});

builder.Services.AddScoped<FeedbackService>();

// Other HTTP Clients

builder.Services.AddHttpClient<IBookingApiClient, BookingApiClient>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7002/");
});

builder.Services.AddHttpClient<ITripApiClient, TripApiClient>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7006/");
});
builder.Services.AddHttpClient<ProfileApiClient>(c =>
{
    c.BaseAddress = new Uri("https://localhost:7001/");
});
//builder.Services.AddHttpClient<CabinReservationApiClient>(c =>
//{
//    c.BaseAddress = new Uri("https://localhost:7003/");
//});

// builder.Services.AddHttpClient<PaymentApiClient>(c =>
// {
//     c.BaseAddress = new Uri("https://localhost:7002/");
// });

builder.Services.AddHttpClient("payment")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
builder.Services.AddScoped<PaymentApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
