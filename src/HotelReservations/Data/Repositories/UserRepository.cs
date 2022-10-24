﻿using HotelReservations.Data.Entities;
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

    public User? ValidateAndGetUser(LoginViewModel viewModel)
    {
        var user = GetUser(viewModel.Username);
        if (user is not null && user.Password == viewModel.Password)
            return user;
        return null;
    }
}