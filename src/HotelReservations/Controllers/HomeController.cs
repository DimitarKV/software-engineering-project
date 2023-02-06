using AutoMapper;
using HotelReservations.Data.Persistence;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Controllers;

public class HomeController : Controller
{
    private readonly HotelDbContext _context;
    private readonly IMapper _mapper;

    public HomeController(HotelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var hotelsMapped = _context.Hotels
            .Include(h => h.Rooms)
            .Select(h => _mapper.Map<HotelViewModel>(h))
            .ToList();
        return View("Index", hotelsMapped);
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult TermsOfService()
    {
        return View();
    }
}