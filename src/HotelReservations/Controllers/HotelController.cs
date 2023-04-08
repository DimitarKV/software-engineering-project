using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers;

public class HotelController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public HotelController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Reserve(HotelViewModel model)
    {
        bool loggedIn = HttpContext.User.Identity!.IsAuthenticated;
        var viewModel = new ReserveViewModel();
        if (!loggedIn)
        {
            viewModel = new ReserveViewModel
            {
                Hotel = model,
                User = new UserViewModel()
            };
        }
        else
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewUser = new UserViewModel();

            if (user != null)
            {
                viewUser = _mapper.Map<UserViewModel>(user);
            }

            viewModel = new ReserveViewModel
            {
                Hotel = model,
                User = viewUser
            };
        }

        return View(viewModel);
    }
}