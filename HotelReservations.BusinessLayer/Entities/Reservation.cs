namespace HotelReservations.BusinessLayer.Entities
{
    public class Reservation
    {
        public Room Room { get; set; }
        public Client Client { get; set; }
        public IEnumerable<Client> Guests { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool isBreakfastIncluded { get; set; }
        public bool isAllInclusive { get; set; }
        public double Price { get; set; }
    }
}
