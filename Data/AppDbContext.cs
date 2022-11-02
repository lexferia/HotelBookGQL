using HotelBookGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookGQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Room>()
                .HasMany(room => room.Bookings)
                .WithOne(booking => booking.Room)
                .HasForeignKey(booking => booking.RoomId);

            modelBuilder
                .Entity<Booking>()
                .HasOne(booking => booking.Room)
                .WithMany(room => room.Bookings)
                .HasForeignKey(room => room.RoomId);
        }
    }
}