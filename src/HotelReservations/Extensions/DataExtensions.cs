using HotelReservations.Data.Initialization;
using HotelReservations.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Extensions;

public static class DataExtensions
{
    public static void AddPersistence(this WebApplicationBuilder builder) 
    {
        var connectionString = builder.Configuration.GetConnectionString("Database");
        builder.Services.AddDbContext<HotelDbContext>(opt =>
            opt.UseSqlServer(connectionString));
    }

    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<HotelDbContext>();
        db!.Database.EnsureCreated();

        var dataInitializer = scope.ServiceProvider.GetService<DataInitializer>();
        
        dataInitializer!.Seed().Wait();
    }
}