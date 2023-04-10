using HotelReservations.Data.Entities;

namespace HotelReservations.ViewModels;

public class HotelViewModelManager
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string  Image { get; set; }
    
    public string Location { get; set; }

    public List<Room> Rooms { get; set; }
}