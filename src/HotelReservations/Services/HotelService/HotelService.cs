using AutoMapper;
using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence;
using HotelReservations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Services.HotelService;

public class HotelService : IHotelService
{
    private readonly HotelDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public HotelService(HotelDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
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

    public async Task BookHotel(ReserveModel model)
    {
        var user = await _userManager.FindByNameAsync(model.User.UserName);
        var hotel = await _context.Hotels.Where(h => h.Id == model.Hotel.Id).Include(h => h.Rooms)!
            .ThenInclude(r => r.Reservations).FirstAsync();
        var room = hotel.Rooms!.FirstOrDefault(r => r.Capacity == model.Properties.Capacity &&
                                                    !r.Reservations.Any(rs =>
                                                        SpansOverlap(rs.ArrivalDate, rs.DepartureDate,
                                                            model.Properties.DateFrom,
                                                            model.Properties.DateFrom.AddDays(model.Properties
                                                                .Duration))));

        if (room is null)
            return;

        var reservation = new Reservation();
        _context.Reservations.Add(reservation);

        reservation.Client = user;
        reservation.Room = room;
        reservation.ArrivalDate = model.Properties.DateFrom;
        reservation.DepartureDate = model.Properties.DateFrom.AddDays(model.Properties.Duration);
        reservation.Price = room.AdultBedPrice;
        reservation.isAllInclusive = true;
        reservation.isBreakfastIncluded = true;

        await _context.SaveChangesAsync();
    }

    private bool SpansOverlap(DateTime firstStart, DateTime firstEnd, DateTime secondStart, DateTime secondEnd)
    {
        var firstSpan = (firstEnd - firstStart).Ticks;
        var secondSpan = (secondEnd - secondStart).Ticks;
        var totalSpan = firstSpan + secondSpan;


        return totalSpan > (new DateTime(Math.Max(firstEnd.Ticks, secondEnd.Ticks)) -
                            new DateTime(Math.Min(firstStart.Ticks, secondStart.Ticks))).Ticks;
    }
}