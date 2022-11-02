using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotelBookGQL.Data;
using HotelBookGQL.Models;

namespace HotelBookGQL.GraphQL.Bookings
{
    public class BookingType : ObjectType<Booking>
    {
        protected override void Configure(IObjectTypeDescriptor<Booking> descriptor)
        {
            descriptor.Description("Represents any booking");
        
            descriptor
                .Field(r => r.Room)
                .ResolveWith<Resolvers>(r => r.GetRoom(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the room which the booking belongs");
        }

        private class Resolvers
        {
            public Room GetRoom([Parent] Booking booking, [ScopedService] AppDbContext context)
            {
                return context.Rooms.FirstOrDefault(b => b.Id.Equals(booking.RoomId));
            }
        }
    }
}