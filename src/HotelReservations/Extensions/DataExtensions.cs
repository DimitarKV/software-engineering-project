using HotelReservations.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Extensions;

public static class DataExtensions
{
    public static void AddPersistence(this WebApplicationBuilder builder, string name = "Database") // TODO: place config in app.json
    {
        var connectionString = builder.Configuration.GetConnectionString(name);
        builder.Services.AddDbContext<HotelDbContext>(opt =>
            opt.UseSqlServer(connectionString));
    }

    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<HotelDbContext>();
        db!.Database.EnsureCreated();
    }
}