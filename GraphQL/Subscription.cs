using HotChocolate;
using HotChocolate.Types;
using HotelBookGQL.Models;

namespace HotelBookGQL.GraphQL
{
    public class Subscription
    {
        [Topic]
        [Subscribe]
        public Room OnRoomAdded([EventMessage] Room room) => room;
    }
}