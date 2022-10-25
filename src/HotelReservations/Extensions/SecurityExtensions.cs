using System.Text;
using HotelReservations.Globals;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservations.Extensions;

public static class SecurityExtensions
{
    /// <summary>
    /// Configures the JWT token header and payload
    /// </summary>
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(GlobalVariables.ExpiryDurationMinutes),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
            
        });
        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        app.UseSession();
        app.Use(async (context, next) =>
        {
            var token = context.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))  
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            await next();
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
    }
}