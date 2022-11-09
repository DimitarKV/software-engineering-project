using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Helpers;
using HotelReservations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AdminController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Panel(int id = 1, int perPage = 5)
    {
        var users = await _userManager.Users
            .Skip((id - 1) * perPage)
            .Take(perPage)
            .Select(user => _mapper.Map<UserViewModel>(user))
            .ToListAsync();


        return View(new AdminPanelViewModel()
        {
            Users = users,
            PaginationProperties = Pagination.CalculateProperties(id,
                await _userManager.Users.CountAsync(),
                perPage)
        });
    }
}