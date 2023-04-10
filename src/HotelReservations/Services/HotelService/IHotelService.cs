using HotelReservations.Models;

namespace HotelReservations.Services.HotelService;

public interface IHotelService
{
    public Task<List<HotelModel>> GetFreeHotels(ReservationQueryModel queryModel);
    public Task BookHotel(ReserveModel model);
}