using System;

namespace HotelBookGQL.GraphQL.Bookings
{
    public record AddBookingInput(DateTime StartDate, DateTime EndDate, int RoomId, int UserId);
}