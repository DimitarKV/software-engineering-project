using HotelReservations.ViewModels;

namespace HotelReservations.Models;

public class ReserveModel
{
    public HotelModel Hotel { get; set; }
    public UserViewModel User { get; set; }
    public ReservationQueryModel Properties { get; set; }
}