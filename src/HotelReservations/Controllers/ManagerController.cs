using HotelReservations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelReservations.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View("CreateHotel");
        }

        
        public IActionResult CreateHotel([FromForm] CreateHotelModel model)
        {
            return View("CreateHotel");
        }
        
        public IActionResult CreateRoomPartial()
        {
            return PartialView("CreateRoomPartial");
        }
    }
}
