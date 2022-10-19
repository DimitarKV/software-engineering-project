namespace HotelReservations.BusinessLayer.Entities
{
    public class Client : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool isAdult { get; set; }
    }
}
