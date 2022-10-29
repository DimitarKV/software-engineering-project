using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Models;

namespace HotelReservations.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(dest => dest.IsAdult,
                conf => conf.MapFrom(src =>
                    DateTime.Now.AddYears(-18).CompareTo(src.DateOfBirth) >= 0));
    }
}