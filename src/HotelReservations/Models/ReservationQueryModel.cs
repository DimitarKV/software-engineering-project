namespace HotelReservations.Models;

public class ReservationQueryModel
{
    public string Destination { get; set; }
    public DateTime DateFrom { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
}