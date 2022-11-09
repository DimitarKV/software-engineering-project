using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using Microsoft.AspNetCore.Antiforgery;

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
        // services.AddRazorPages()
        //     .AddRazorPagesOptions(options =>
        //     {
        //     });
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
    }
}