using BokPalace.Application.Rooms.Dtos;
using BokPalace.Application.Rooms.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BokPalace.Api.UseCases;

public class RoomEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Rooms")
            .WithTags("Rooms");

        group.MapGet("", Get);
    }
    private static async Task<Ok<IReadOnlyCollection<RoomDto>>> Get(ISender sender)
        => TypedResults.Ok(await sender.Send(new GetRooms.Query()));

}
