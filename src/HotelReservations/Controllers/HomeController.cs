using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HotelReservations.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservations.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Search(ReservationQueryViewModel viewModel)
    {
        Console.WriteLine(viewModel.Destination + " " + viewModel.DateFrom);
        return View(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}