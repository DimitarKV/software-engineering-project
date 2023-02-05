using HotelReservations.Data.Entities.Enums;

namespace HotelReservations.Models;

public class CreateRoomModel
{
    public int Capacity { get; set; }
    public RoomTypeEnum Type { get; set; }
    public double AdultBedPrice { get; set; }
    public double KidBedPrice { get; set; }
    public int Number { get; set; }
}