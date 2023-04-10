using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Models;
using HotelReservations.ViewModels;

namespace HotelReservations.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<RegisterModel, User>()
            .ForMember(dest => dest.IsAdult,
                conf => conf.MapFrom(src =>
                    DateTime.Now.AddYears(-18).CompareTo(src.DateOfBirth) >= 0));
        CreateMap<User, UserViewModel>();
        CreateMap<Hotel, BasicHotelViewModel>();
        CreateMap<CreateRoomModel, Room>();

        CreateMap<Hotel, HotelModel>()
            .ForMember(dest => dest.RoomCount,
                conf => conf.MapFrom(src => src.Rooms!.Count()));
        CreateMap<Hotel, HotelManagerViewModel>();
        CreateMap<Room, ManagerRoomViewModel>();
        CreateMap<Reservation, ManagerReservationViewModel>()
            .ForMember(dest => dest.ClientFirstName,
                conf => conf.MapFrom(src => src.Client!.FirstName));
    }
}