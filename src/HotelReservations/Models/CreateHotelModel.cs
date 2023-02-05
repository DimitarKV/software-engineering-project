namespace HotelReservations.Models;

public class CreateHotelModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Location { get; set; }
    public IEnumerable<CreateRoomModel> Rooms { get; set; }
}