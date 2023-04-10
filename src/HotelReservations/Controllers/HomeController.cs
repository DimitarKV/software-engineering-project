using AutoMapper;
using HotelReservations.Data.Persistence;
using HotelReservations.Models;
using HotelReservations.Services.HotelService;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Controllers;

public class HomeController : Controller
{
    private readonly HotelDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHotelService _hotelService;

    public HomeController(HotelDbContext context, IMapper mapper, IHotelService hotelService)
    {
        _context = context;
        _mapper = mapper;
        _hotelService = hotelService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("Index", new HomePageModel() {QueryForm = new ReservationQueryModel() {DateFrom = DateTime.Now}});
    }

    [HttpPost]
    public async Task<IActionResult> Index(HomePageModel model)
    {
        var hotels = await _hotelService.GetFreeHotels(model.QueryForm);
        return View("Index", new HomePageModel() {Hotels = hotels, QueryForm = model.QueryForm});
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