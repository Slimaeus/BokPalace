using BokPalace.Application.Rooms.Commands;
using BokPalace.Application.Rooms.Dtos;
using BokPalace.Application.Rooms.Queries;
using BokPalace.Domain.Rooms;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BokPalace.Api.UseCases;

public class RoomEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Rooms")
            .WithTags("Rooms");

        group.MapGet("", Get);
        group.MapGet("{id:guid}", GetById)
            .WithName(nameof(GetById));
        group.MapPost("", Post);
    }
    private static async Task<Ok<IReadOnlyCollection<RoomDto>>> Get(ISender sender)
        => TypedResults.Ok(await sender.Send(new GetRooms.Query()));
    private static async Task<Ok<RoomDto>> GetById(ISender sender, Guid id)
        => TypedResults.Ok(await sender.Send(new GetRoomById.Query(new RoomId(id))));
    private static async Task<Created<RoomDto>> Post(ISender sender, CreateRoom.Command request, HttpContext httpContext)
    {
        var id = await sender.Send(request);
        var roomDto = await sender.Send(new GetRoomById.Query(id));
        var domain = httpContext.Request.GetDisplayUrl();
        return TypedResults.Created($"{domain}/{id.Value}", roomDto);
    }

}
