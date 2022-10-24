using HotelReservations.Data.Extensions;
using HotelReservations.Data.Persistence;
using HotelReservations.Data.Persistence.Interfaces;
using HotelReservations.Data.Repositories;
using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Extensions;
using HotelReservations.Services.Security;
using HotelReservations.Services.Security.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddPersistence();
builder.Services.AddTransient<IHotelDbContext, HotelDbContext>();
builder.Services.AddTransient<HotelDbContext, HotelDbContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.AddSecurity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSecurity();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.EnsureDatabaseCreated();
app.Run();