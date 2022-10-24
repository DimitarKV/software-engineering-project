namespace HotelReservations.Data.Entities;

public class Employee : Entity<int>
{
    public string Surname { get; set; }
    public string EGN { get; set; }
    public Hotel Hotel { get; set; }
}