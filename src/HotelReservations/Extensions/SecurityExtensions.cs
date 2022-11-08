using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;

namespace HotelReservations.Extensions;

public static class SecurityExtensions
{
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 0;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;

        }).AddEntityFrameworkStores<HotelDbContext>();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseHttpsRedirection();
    }
}