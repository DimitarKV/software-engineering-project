namespace HotelReservations.ViewModels;

public class ManagerRoomViewModel
{
    public int Number { get; set; }
    public List<ManagerReservationViewModel> Reservations { get; set; }
}