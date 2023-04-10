namespace HotelReservations.Models;

public class HomePageModel
{
    public ReservationQueryModel? QueryForm { get; set; }
    public List<HotelModel> Hotels { get; set; }

    public HomePageModel()
    {
        QueryForm = new ReservationQueryModel();
        Hotels = new List<HotelModel>();
    }
}