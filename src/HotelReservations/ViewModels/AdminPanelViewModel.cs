using HotelReservations.Helpers;

namespace HotelReservations.ViewModels;

public class AdminPanelViewModel
{
    public IEnumerable<UserViewModel> Users { get; set; }
    public PaginationProperties PaginationProperties { get; set; }
}