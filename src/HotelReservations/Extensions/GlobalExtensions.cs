using HotelReservations.Data.Persistence;
using HotelReservations.Data.Persistence.Interfaces;
using HotelReservations.Data.Repositories;
using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Services.Security;
using HotelReservations.Services.Security.Interfaces;
using HotelReservations.Services.UserService;
using HotelReservations.Services.UserService.Interfaces;

namespace HotelReservations.Extensions;

public static class GlobalExtensions
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IHotelDbContext, HotelDbContext>();
        builder.Services.AddTransient<HotelDbContext, HotelDbContext>();
        
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        
        builder.Services.AddTransient<ITokenService, TokenService>();
        
        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    }
}