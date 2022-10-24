using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using HotelReservations.Data.Repositories.Interfaces;

namespace HotelReservations.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HotelDbContext _context;

    public UserRepository(HotelDbContext context)
    {
        _context = context;
    }

    public User? GetUser(string username)
    {
        return _context.Users.Find(username);
    }
}