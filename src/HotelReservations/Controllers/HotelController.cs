using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Models;
using HotelReservations.Services.HotelService;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers;

public class HotelController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IHotelService _hotelService;

    public HotelController(UserManager<User> userManager, IMapper mapper, IHotelService hotelService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _hotelService = hotelService;
    }

    [HttpPost]
    public async Task<IActionResult> BookDetails(ReserveModel model)
    {
        bool loggedIn = HttpContext.User.Identity!.IsAuthenticated;
        if (!loggedIn)
        {
            model.User = new UserViewModel();
        }
        else
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewUser = new UserViewModel();

            if (user != null)
            {
                viewUser = _mapper.Map<UserViewModel>(user);
            }

            model.User = viewUser;
        }

        return View("Reserve", model);
    }

    [HttpPost]
    public async Task<IActionResult> BookHotel(ReserveModel model)
    {
        await _hotelService.BookHotel(model);
        return RedirectToAction("Index", "Home");
    }

}