using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelReservations.Data.Entities;
using HotelReservations.Globals;
using HotelReservations.Services.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HotelReservations.Services.Security;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string BuildToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"], 
            _configuration["Jwt:Audience"], 
            claims,
            expires: DateTime.Now.AddMinutes(GlobalVariables.ExpiryDurationMinutes), 
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public bool ValidateToken(string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}