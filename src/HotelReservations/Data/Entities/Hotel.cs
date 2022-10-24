namespace HotelReservations.Data.Entities;

public class Hotel : Entity<int>
{
    public IEnumerable<Reservation> Reservations { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    public IEnumerable<User> Employees { get; set; }
    
}