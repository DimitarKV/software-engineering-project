using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using HotelReservations.Helpers.Cloudinary;
using HotelReservations.Models;
using HotelReservations.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly HotelDbContext _context;
    private readonly IImageUploader _uploader;
    private readonly IMapper _mapper;

    public ManagerService(HotelDbContext context, IImageUploader uploader, IMapper mapper)
    {
        _context = context;
        _uploader = uploader;
        _mapper = mapper;
    }

    public async Task CreateHotelAsync(CreateHotelModel model, string username)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserName == username);
        var result = await _uploader.UploadImageAsync(model.Image.Name, model.Image);
        var hotel = new Hotel
        {
            Name = model.Name, Description = model.Description, Location = model.Location,
            Image = result.Uri.ToString(), User = user
        };
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task<CreateRoomModel> GenerateRoomTemplateAsync(string username)
    {
        var user = _context.Users.Include(u => u.Hotels).First(x => x.UserName == username);
        var hotels = user.Hotels.ToList();
        var hotelsMapped = new List<BasicHotelViewModel>();

        foreach (var kirech in hotels)
        {
            hotelsMapped.Add(_mapper.Map<BasicHotelViewModel>(kirech));
        }

        return new CreateRoomModel() {CurrentUserHotels = hotelsMapped};
    }

    public async Task CreateRoomAsync(CreateRoomModel model)
    {
        var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == model.HotelId);
        var room = _mapper.Map<Room>(model);
        room.Hotel = hotel!;
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }
}