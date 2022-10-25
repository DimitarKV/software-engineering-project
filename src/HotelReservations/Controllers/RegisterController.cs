using HotelReservations.Data.Repositories.Interfaces;
using HotelReservations.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers;

public class RegisterController : Controller
{
    private readonly IUserRepository _userRepository;

    public RegisterController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register(RegisterViewModel viewModel)
    {
        _userRepository.Register(viewModel);
        return Redirect("/");
    }
}