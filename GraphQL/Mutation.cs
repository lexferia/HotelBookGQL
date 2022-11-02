using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using HotelBookGQL.Data;
using HotelBookGQL.GraphQL.Bookings;
using HotelBookGQL.GraphQL.Rooms;
using HotelBookGQL.Models;

namespace HotelBookGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddRoomPayLoad> AddRoomAsync(
            AddRoomInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var room = new Room {
                Name = input.Name,
                Type = input.Type
            };

            context.Rooms.Add(room);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnRoomAdded), room, cancellationToken);

            return new AddRoomPayLoad(room);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddBookingPayLoad> AddBookingAsync(AddBookingInput input,
            [ScopedService] AppDbContext context)
        {
            var booking = new Booking {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                RoomId = input.RoomId,
                UserId = input.UserId
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            return new AddBookingPayLoad(booking);
        }
    }
}