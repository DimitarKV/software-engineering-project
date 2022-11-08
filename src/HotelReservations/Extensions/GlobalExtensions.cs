using HotelReservations.Data.Initialization;
using HotelReservations.Data.Persistence;

namespace HotelReservations.Extensions;

public static class GlobalExtensions
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<HotelDbContext, HotelDbContext>();
        
        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddTransient<DataInitializer, DataInitializer>();
    }
}