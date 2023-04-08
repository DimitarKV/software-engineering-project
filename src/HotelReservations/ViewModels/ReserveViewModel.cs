using System.Security.Permissions;
using HotelReservations.Data.Entities;

namespace HotelReservations.ViewModels;

public class ReserveViewModel
{
    public HotelViewModel Hotel { get; set; }
    public UserViewModel User { get; set; }
}