using AutoMapper;
using HotelReservations.Data.Persistence;
using HotelReservations.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Services.HotelService;

public class HotelService : IHotelService
{
    private readonly HotelDbContext _context;
    private readonly IMapper _mapper;

    public HotelService(HotelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HotelModel>> GetFreeHotels(ReservationQueryModel queryModel)
    {
        var reservationArrivalDate = queryModel.DateFrom;
        var reservationDepartureDate = queryModel.DateFrom.AddDays(queryModel.Duration);
        var reservationTotalSpan = (reservationDepartureDate - reservationArrivalDate);

        var hotels = await _context.Hotels
            .Where(h => h.Location == queryModel.Destination)
            .Include(h => h.Rooms)!
            .ThenInclude(r => r.Reservations)
            .ToListAsync();

        var filteredHotels = hotels.Where(h =>
            h.Rooms!.Any(r =>
                r.Reservations.Any(rv =>
                {
                    var currentReservationArrivalDate = rv.ArrivalDate;
                    var currentReservationDepartureDate = rv.DepartureDate;
                    var currentReservationTotalSpan =
                        (currentReservationDepartureDate - currentReservationArrivalDate);

                    var span = currentReservationTotalSpan + reservationTotalSpan;

                    return span.Ticks > (new DateTime(Math.Max(reservationDepartureDate.Ticks,
                                       currentReservationDepartureDate.Ticks)) -
                                   new DateTime(Math.Min(reservationArrivalDate.Ticks,
                                       currentReservationArrivalDate.Ticks))).Ticks;
                }) == false))
            .Select(h => _mapper.Map<HotelModel>(h)).ToList();

        return filteredHotels;
    }
}