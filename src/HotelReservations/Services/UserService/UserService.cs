using HotelReservations.Data.Entities;
using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Models;
using HotelReservations.Services.Security.Interfaces;
using HotelReservations.Services.UserService.Interfaces;

namespace HotelReservations.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public User? ValidateUser(LoginViewModel viewModel)
    {
        if (string.IsNullOrEmpty(viewModel.Username) || string.IsNullOrEmpty(viewModel.Password))
        {
            return null;
        }

        var user = _userRepository.GetUser(viewModel.Username);
        
        if (user is not null && user.Password == viewModel.Password)
            return user;
        
        return null;
    }

    public bool UserExists(string viewModelUsername)
    {
        return _userRepository.GetUser(viewModelUsername) is not null;
    }

    public User? RegisterUser(User user)
    {
        return _userRepository.SaveUser(user);
    }
}