namespace HotelReservations.Data.Entities;

public class Hotel : Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Location { get; set; }
    public IEnumerable<Room>? Rooms { get; set; }
    public User Owner { get; set; }
}