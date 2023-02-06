using AutoMapper;
using HotelReservations.Data.Persistence;
using HotelReservations.Helpers.Cloudinary;
using HotelReservations.Models;
using HotelReservations.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservations.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ManagerController : Controller
    {
        private readonly IImageUploader _uploader;
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;

        public ManagerController(IImageUploader uploader, HotelDbContext context, IMapper mapper,
            IManagerService managerService)
        {
            _uploader = uploader;
            _context = context;
            _mapper = mapper;
            _managerService = managerService;
        }
        
        [HttpGet]
        public IActionResult CreateHotel()
        {
            return View("CreateHotel");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoom()
        {
            var username = User.Identity!.Name;
            var model = await _managerService.GenerateRoomTemplateAsync(username!);
            return View("CreateRoom", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromForm] CreateHotelModel model)
        {
            var username = User.Identity!.Name;
            await _managerService.CreateHotelAsync(model, username!);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomModel model)
        {
            var username = User.Identity!.Name;
            await _managerService.CreateRoomAsync(model);
            
            return RedirectToAction("Index", "Home");
        }
    }
}