using HotelReservations.Data.Entities.Enums;

namespace HotelReservations.Data.Entities
{
    public class Room : Entity<int>
    {
        public int Capacity { get; set; }
        public RoomTypeEnum Type { get; set; }
        public bool isFree { get; set; } = true; 
        public double AdultBedPrice { get; set; }
        public double KidBedPrice { get; set; }
        public int Number { get; set; }
        public Hotel Hotel { get; set; }
    }
}
