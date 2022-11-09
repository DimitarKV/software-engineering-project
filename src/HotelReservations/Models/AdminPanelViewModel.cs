using HotelReservations.Helpers;

namespace HotelReservations.Models;

public class AdminPanelViewModel
{
    public IEnumerable<UserViewModel> Users { get; set; }
    public PaginationProperties PaginationProperties { get; set; }
}