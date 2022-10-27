using HotelReservations.Data.Entities;
using HotelReservations.Models;

namespace HotelReservations.Services.UserService.Interfaces;

public interface IUserService
{
    User? ValidateUser(HttpResponse response, LoginViewModel viewModel);
    bool UserExists(string viewModelUsername);
}