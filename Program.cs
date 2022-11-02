using GraphQL.Server.Ui.Voyager;
using HotelBookGQL.Data;
using HotelBookGQL.GraphQL;
using HotelBookGQL.GraphQL.Bookings;
using HotelBookGQL.GraphQL.Rooms;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => 
    opt.UseSqlServer(configuration.GetConnectionString("Default"))
);

builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>()
            .AddType<RoomType>()
            .AddType<BookingType>()
            .AddFiltering()
            .AddSorting()
            .AddInMemorySubscriptions();

var app = builder.Build();

app.MapGraphQL();

app.UseGraphQLVoyager(options: new VoyagerOptions() 
{ 
    GraphQLEndPoint = "/graphql"
});

app.UseWebSockets();

PrepDb.PrepPopulation(app);

app.Run();
