using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View("CreateHotel");
        }
    }
}
