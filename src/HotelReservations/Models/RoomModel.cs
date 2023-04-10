using HotelReservations.Data.Entities;
using HotelReservations.Data.Entities.Enums;

namespace HotelReservations.Models;

public class RoomModel
{
    public int Capacity { get; set; }
    
    public RoomTypeEnum Type { get; set; }
    
    public bool isFree { get; set; }
    
    public double AdultBedPrice { get; set; }
    
    public double KidBedPrice { get; set; }
    
    public int Number { get; set; }
    
    public Hotel Hotel { get; set; }
}