using HotelReservations.Data.Extensions;
using HotelReservations.Data.Persistence;
using HotelReservations.Data.Persistence.Interfaces;
using HotelReservations.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddPersistence();
builder.Services.AddTransient<IHotelDbContext, HotelDbContext>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.EnsureDatabaseCreated();

app.UseSecurity();
app.Run();