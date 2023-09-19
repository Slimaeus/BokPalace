using BokPalace.Application.Seeding.Commands;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BokPalace.Api.UseCases;

public class SeedingEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/seeding");

        group.MapPost("", Post);
    }
    private static async Task<NoContent> Post(ISender sender)
    {
        await sender.Send(new SeedData.Command());
        return TypedResults.NoContent();
    }
}
