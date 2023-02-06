using HotelReservations.Models;

namespace HotelReservations.Services.ManagerService;

public interface IManagerService
{
    Task CreateHotelAsync(CreateHotelModel model, string username);
    Task<CreateRoomModel> GenerateRoomTemplateAsync(string username);

    Task CreateRoomAsync(CreateRoomModel model);
}