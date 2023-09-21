using BokPalace.Application.Palaces.Commands;
using BokPalace.Application.Palaces.Dtos;
using BokPalace.Application.Palaces.Queries;
using BokPalace.Domain.Rooms;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BokPalace.Api.UseCases;

public class PalaceEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Palaces")
            .WithTags("Palaces");

        group.MapGet("", Get);
        group.MapGet("{id:guid}", GetById);
        group.MapPost("", Post);
        group.MapPut("{id:guid}", Put);
        group.MapDelete("{id:guid}", Delete);
    }
    private static async Task<Ok<IReadOnlyCollection<PalaceDto>>> Get(ISender sender)
        => TypedResults.Ok(await sender.Send(new GetPalaces.Query()));
    private static async Task<Ok<PalaceDto>> GetById(ISender sender, Guid id)
        => TypedResults.Ok(await sender.Send(new GetPalaceById.Query(new PalaceId(id))));
    private static async Task<Created<PalaceDto>> Post(ISender sender, CreatePalace.Command request, HttpContext httpContext)
    {
        var id = await sender.Send(request);
        var roomDto = await sender.Send(new GetPalaceById.Query(id));
        var domain = httpContext.Request.GetDisplayUrl();
        return TypedResults.Created($"{domain}/{id}", roomDto);
    }
    private static async Task<Results<NoContent, BadRequest>> Put(ISender sender, Guid id, UpdatePalace.Command request)
    {
        if (!id.Equals(request.Id.Value))
            return TypedResults.BadRequest();
        await sender.Send(request)
            .ConfigureAwait(false);

        return TypedResults.NoContent();
    }
    private static async Task<Ok<PalaceDto>> Delete(ISender sender, Guid id)
    {
        var roomDto = await sender.Send(new GetPalaceById.Query(new PalaceId(id)));
        await sender.Send(new DeletePalace.Command(new PalaceId(id)));
        return TypedResults.Ok(roomDto);
    }
}
