using HotelReservations.Data.Entities;

namespace HotelReservations.Services.Security.Interfaces;

public interface ITokenService
{
    string? BuildToken(User user);
    bool ValidateToken(string token);
}