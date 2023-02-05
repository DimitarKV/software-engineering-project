using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Helpers;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AdminController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Panel(int page = 1, int perPage = 5)
    {
        var users = await _userManager.Users
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .Select(user => _mapper.Map<UserViewModel>(user))
            .ToListAsync();

        ViewData["QueryParameters"] = new Dictionary<string, string>
        {
            {"Page", page.ToString()},
            {"PerPage", perPage.ToString()}
        };

        return View(new AdminPanelViewModel()
        {
            Users = users,
            PaginationProperties = Pagination.CalculateProperties(page,
                await _userManager.Users.CountAsync(),
                perPage)
        });
    }
}