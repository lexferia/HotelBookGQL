using System;
using System.Linq;
using HotelBookGQL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookGQL.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var contextFactory = serviceScope.ServiceProvider.GetService<IDbContextFactory<AppDbContext>>();
                using (var context = contextFactory.CreateDbContext())
                {
                    SeedData(context);
                }
            }
        }

        private static void SeedData(AppDbContext context)
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }

            if (!context.Rooms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                const string RoomA1Ordinary = "Room A1 - Ordinary";
                const string RoomA2Ordinary = "Room A2 - Ordinary";
                const string RoomC2Deluxe = "Room C2 - Deluxe";

                context.AddRange(
                    new Room { Name = RoomA1Ordinary, Type = 1, Status = 1 }, //Single
                    new Room { Name = RoomA2Ordinary, Type = 1, Status = 1 }, //Single
                    new Room { Name = "Room B1 - Deluxe", Type = 2, Status = 1 }, //Single
                    new Room { Name = "Room B2 - Deluxe", Type = 2, Status = 1 }, //Single
                    new Room { Name = "Room C1 - Deluxe", Type = 2, Status = 1 }, //Single
                    new Room { Name = RoomC2Deluxe, Type = 2, Status = 1 }, //Single
                    new Room { Name = "Room D1 - Ordinary", Type = 4, Status = 1 },//Family
                    new Room { Name = "Room D2 - Ordinary", Type = 4, Status = 1 }, //Family
                    new Room { Name = "Room E1 - Deluxe", Type = 8, Status = 1 }, //Family
                    new Room { Name = "Room E2 - Deluxe", Type = 8, Status = 1 } //Family
                );

                context.SaveChanges();

                var rooms = context.Rooms.Where(room => room.Name.Equals(RoomA1Ordinary) || 
                                                        room.Name.Equals(RoomA2Ordinary) || 
                                                        room.Name.Equals(RoomC2Deluxe))
                                         .AsEnumerable();

                var roomA1OrdinaryId = rooms.FirstOrDefault(room => room.Name.Equals(RoomA1Ordinary)).Id;
                var roomA2OrdinaryId = rooms.FirstOrDefault(room => room.Name.Equals(RoomA2Ordinary)).Id;
                var roomC2DeluxeId = rooms.FirstOrDefault(room => room.Name.Equals(RoomC2Deluxe)).Id;

                context.AddRange(
                    new Booking { UserId = 1, RoomId = roomA1OrdinaryId, StartDate = DateTime.Parse("2022-10-18T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-25T01:39:55.6200000"), Status = 4 },
                    new Booking { UserId = 5, RoomId = roomA1OrdinaryId, StartDate = DateTime.Parse("2022-10-25T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-28T01:39:55.6200000"), Status = 1 },
                    new Booking { UserId = 3, RoomId = roomA2OrdinaryId, StartDate = DateTime.Parse("2022-10-16T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-17T01:39:55.6200000"), Status = 8 },
                    new Booking { UserId = 2, RoomId = roomA2OrdinaryId, StartDate = DateTime.Parse("2022-10-18T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-20T01:39:55.6200000"), Status = 4 },
                    new Booking { UserId = 4, RoomId = roomC2DeluxeId, StartDate = DateTime.Parse("2022-10-18T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-21T01:39:55.6200000"), Status = 4 },
                    new Booking { UserId = 6, RoomId = roomC2DeluxeId, StartDate = DateTime.Parse("2022-10-21T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-22T01:39:55.6200000"), Status = 1 },
                    new Booking { UserId = 7, RoomId = roomC2DeluxeId, StartDate = DateTime.Parse("2022-10-22T01:39:55.6200000"), EndDate = DateTime.Parse("2022-10-25T01:39:55.6200000"), Status = 1 }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}