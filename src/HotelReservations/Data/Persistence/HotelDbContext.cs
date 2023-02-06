using HotelReservations.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelReservations.Data.Persistence;

public class HotelDbContext : IdentityDbContext<User, Role, int>
{
    // public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    public HotelDbContext()
    {
        
    }

    public HotelDbContext(DbContextOptions options) : base(options)
    {
    }
}