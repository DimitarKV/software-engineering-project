using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Models;

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
        return _context.Users.FirstOrDefault(user => user.Username == username);
    }

    public User? SaveUser(User user)
    {
        var entity = _context.Users.Add(user).Entity;
        _context.SaveChanges();
        return entity;
    }
}