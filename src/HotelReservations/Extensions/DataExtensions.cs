using HotelReservations.Data.Initialization;
using HotelReservations.Data.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Extensions;

public static class DataExtensions
{
    public static void AddPersistence(this WebApplicationBuilder builder) 
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Database"));
        connectionStringBuilder.UserID = builder.Configuration["DbUser"];
        connectionStringBuilder.Password = builder.Configuration["DbPassword"];
        
        builder.Services.AddDbContext<HotelDbContext>(opt =>
            opt.UseSqlServer(connectionStringBuilder.ConnectionString));
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