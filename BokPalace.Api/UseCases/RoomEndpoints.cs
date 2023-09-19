using BokPalace.Application.Rooms.Dtos;
using BokPalace.Application.Rooms.Queries;
using BokPalace.Domain.Rooms;
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
        group.MapGet("{id:guid}", GetById);
    }
    private static async Task<Ok<IReadOnlyCollection<RoomDto>>> Get(ISender sender)
        => TypedResults.Ok(await sender.Send(new GetRooms.Query()));
    private static async Task<Ok<RoomDto>> GetById(ISender sender, Guid id)
        => TypedResults.Ok(await sender.Send(new GetRoomById.Query(new RoomId(id))));

}
