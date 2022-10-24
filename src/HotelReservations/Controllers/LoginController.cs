using HotelReservations.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test(LoginViewModel viewModel)
        {
            Console.WriteLine(viewModel.Email + " " + viewModel.Password);
            return Redirect("/Home");
        }
    }
}
