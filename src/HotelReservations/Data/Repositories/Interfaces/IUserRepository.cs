using HotelReservations.Data.Entities;

namespace HotelReservations.Data.Repositories.Interfaces;

public interface IUserRepository
{
    User? GetUser(string username);
}