using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using HotelBookGQL.Data;
using HotelBookGQL.Models;

namespace HotelBookGQL.GraphQL.Rooms
{
    public class RoomType : ObjectType<Room>
    {
        protected override void Configure(IObjectTypeDescriptor<Room> descriptor)
        {
            descriptor.Description("Represents any room within the hotel");
        
            descriptor
                .Field(r => r.Bookings)
                .ResolveWith<Resolvers>(r => r.GetBookings(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of bookings for this room");
        }

        private class Resolvers
        {
            public IQueryable<Booking> GetBookings([Parent] Room room, [ScopedService] AppDbContext context)
            {
                return context.Bookings.Where(b => b.RoomId.Equals(room.Id));
            }
        }
    }
}