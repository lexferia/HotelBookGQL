using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace HotelBookGQL.Models
{
    [GraphQLDescription("Represents any room within the hotel")]
    public class Room
    {
        [Key]
        [GraphQLDescription("Room identifier")]
        public int Id { get; set; }

        [Required]
        [GraphQLDescription("Room name")]
        public string Name { get; set; }

        [Required]
        [GraphQLDescription("Room type. 1 = Single - Ordinary, 2 = Single - Deluxe, 4 = Family - Ordinary, 8 = Family - Deluxe")]
        public int Type { get; set; } // Single - Ordinary || Single - Deluxe || Family - Ordinary || Family - Deluxe
        
        [Required]
        [GraphQLDescription("Room status. 1 = Available, 2 = Unavailable")]
        public int Status { get; set; } = 1; // Available || Unavailable

        public string Remarks { get;set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}