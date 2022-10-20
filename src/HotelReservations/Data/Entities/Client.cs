using HotelReservations.Models;

namespace HotelReservations.Data.Entities
{
    public class Client : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsAdult { get; set; }
    }
}
