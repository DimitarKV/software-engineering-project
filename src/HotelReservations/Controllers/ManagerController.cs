using HotelReservations.Helpers.Cloudinary;
using HotelReservations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelReservations.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IImageUploader _uploader;

        public ManagerController(IImageUploader uploader)
        {
            _uploader = uploader;
        }

        public IActionResult Index()
        {
            return View("CreateHotel");
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromForm] CreateHotelModel model)
        {
            var result = await _uploader.UploadImageAsync(model.Image.Name, model.Image);
            Console.WriteLine(result.Uri.ToString());
            return RedirectToAction("Index");
        }
        
        public IActionResult CreateRoomPartial()
        {
            return PartialView("_CreateRoom");
        }
        
        public IActionResult CreateRoomInfoPartial()
        {
            return PartialView("_RoomInfo");
        }
    }
}
