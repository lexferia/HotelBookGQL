using System;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace HotelBookGQL.Models
{
    [GraphQLDescription("Represents any booking")]
    public class Booking
    {
        [Key]
        [GraphQLDescription("Booking identifier")]
        public int Id { get; set; }

        [Required]
        [GraphQLDescription("Represents user's Id who booked")]
        public int UserId { get; set; }

        [Required]
        [GraphQLDescription("Represents rooms's Id being booked")]
        public int RoomId { get; set; }

        [Required]
        [GraphQLDescription("Represents Date and Time of the Check-in")]
        public DateTime StartDate { get; set; }

        [Required]
        [GraphQLDescription("Represents Date and Time of the Check-out")]
        public DateTime EndDate { get; set; }

        [Required]
        [GraphQLDescription("Booking status. 1 = Reserved, 2 = Cancelled, 4 = Check-in, 8 = Check-out")]
        public int Status { get; set; } = 1; // Reserved || Cancelled || Check-in || Check-out
    
        public Room Room { get; set; }
    }
}