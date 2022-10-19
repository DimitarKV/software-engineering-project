using HotelReservations.BusinessLayer.Entities.Enums;

namespace HotelReservations.BusinessLayer.Entities
{
    public class Room : Entity<int>
    {
        public int Capacity { get; set; }
        public RoomTypeEnum Type { get; set; }
        public bool isFree { get; set; }
        public double AdultBedPrice { get; set; }
        public double KidBedPrice { get; set; }
        public int Number { get; set; }
    }
}
