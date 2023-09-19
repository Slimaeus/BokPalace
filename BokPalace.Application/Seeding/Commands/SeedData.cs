using BokPalace.Domain.Rooms;
using BokPalace.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BokPalace.Application.Seeding.Commands;

public static class SeedData
{
    public sealed record Command : IRequest<Unit>;
    sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Handler(ApplicationDbContext applicationDbContext)
            => _applicationDbContext = applicationDbContext;
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await SeedRooms();
            return Unit.Value;
        }

        private async Task SeedRooms()
        {
            if (await _applicationDbContext.Rooms.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("../../BokPalace/BokPalace.Infrastructure/Persistence/Seeding/Rooms.json");
            var routes = JsonSerializer.Deserialize<List<Room>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
            await _applicationDbContext.AddRangeAsync(routes);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
