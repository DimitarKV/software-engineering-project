using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using HotelReservations.Helpers.Cloudinary;
using HotelReservations.Models;
using HotelReservations.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly IImageUploader _uploader;
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public ManagerController(IImageUploader uploader, HotelDbContext context, IMapper mapper)
        {
            _uploader = uploader;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult CreateHotel()
        {
            return View("CreateHotel");
        }

        public IActionResult CreateRoom()
        {
            var roomModel = new CreateRoomModel();
            var username = User.Identity.Name;
            var user = _context.Users.Include(u => u.Hotels).Where(x => x.UserName == username).First();
            var hotels = user.Hotels.ToList();
            var hotelsMapped = new List<BasicHotelViewModel>();

            foreach (var kirech in hotels)
            {
                hotelsMapped.Add(_mapper.Map<BasicHotelViewModel>(hotels));    
            }
            return View("CreateRoom", new CreateRoomModel { CurrentUserHotels = hotelsMapped });
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromForm] CreateHotelModel model)
        {
            var username = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == username).First();

            var result = await _uploader.UploadImageAsync(model.Image.Name, model.Image);
            var hotel = new Hotel{ Name = model.Name, Description = model.Description, Location = model.Location, Image = result.Uri.ToString(), User = user };
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
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
