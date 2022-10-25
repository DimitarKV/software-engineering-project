using System.Reflection;
using FluentValidation;
using HotelReservations.Behaviours;
using HotelReservations.Data.Persistence;
using HotelReservations.Data.Persistence.Interfaces;
using HotelReservations.Data.Repositories;
using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Services.Security;
using HotelReservations.Services.Security.Interfaces;
using MediatR;

namespace HotelReservations.Data.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(assemblies);
        
        services.AddTransient<IHotelDbContext, HotelDbContext>();
        services.AddTransient<HotelDbContext, HotelDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddAutoMapper(assemblies);
        
        return services;
    }
}