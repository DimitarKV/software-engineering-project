using HotelReservations.Data.Entities;
using HotelReservations.Data.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Data.Persistence;

public class HotelDbContext : DbContext, IHotelDbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    public HotelDbContext()
    {
        
    }

    public HotelDbContext(DbContextOptions options) : base(options)
    {
    }
}