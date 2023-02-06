using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservations.Data.Entities;

public class Hotel : Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Location { get; set; }
    // public IEnumerable<Reservation> Reservations { get; set; }
    public IEnumerable<Room> Rooms { get; set; }

    [NotMapped]
    public IEnumerable<User> Employees { get; set; }
    public User User { get; set; }
}