using HotelReservations.Data.Entities;
using HotelReservations.MediatR.Commands;
using HotelReservations.Models;

namespace HotelReservations.Data.Repositories.Interfaces;

public interface IUserRepository
{
    User? GetUser(string username);
    User? ValidateAndGetUser(LoginViewModel viewModel);
    User? Register(RegisterViewModel viewModel);
    Task<User?> ValidateAndGetUserAsync(LoginUserCommand request);
}