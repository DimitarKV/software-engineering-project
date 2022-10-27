using HotelReservations.Data.Entities;
using HotelReservations.Models;

namespace HotelReservations.Data.Repositories.Interfaces;

public interface IUserRepository
{
    User? GetUser(string username);
}