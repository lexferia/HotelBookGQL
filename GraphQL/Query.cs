using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using HotelBookGQL.Data;
using HotelBookGQL.Models;

namespace HotelBookGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Room> GetRoom([ScopedService] AppDbContext context)
        {
            return context.Rooms;
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Booking> GetBooking([ScopedService] AppDbContext context)
        {
            return context.Bookings;
        }
    }
}