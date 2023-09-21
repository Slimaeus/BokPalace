using BokPalace.Domain.Palaces;
using BokPalace.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace BokPalace.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextInitializer(ApplicationDbContext applicationDbContext)
        => _applicationDbContext = applicationDbContext;
    public async Task InitialiseAsync()
    {
        try
        {
            await _applicationDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Migration error");
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Seeding error");
        }
    }

    public async Task TrySeedAsync()
    {
        await SeedPalaces();
        await SeedRooms();
    }
    private async Task SeedPalaces()
    {
        if (await _applicationDbContext.Palaces.AnyAsync()) return;

        var json = await File.ReadAllTextAsync("../../BokPalace/BokPalace.Infrastructure/Persistence/Seeding/Palaces.json");
        var palaces = JsonSerializer.Deserialize<List<Palace>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })!;
        await _applicationDbContext.AddRangeAsync(palaces);
        await _applicationDbContext.SaveChangesAsync();
    }
    private async Task SeedRooms()
    {
        if (await _applicationDbContext.Rooms.AnyAsync()) return;

        var json = await File.ReadAllTextAsync("../../BokPalace/BokPalace.Infrastructure/Persistence/Seeding/Rooms.json");
        var rooms = JsonSerializer.Deserialize<List<Room>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })!;
        var palaceIds = _applicationDbContext.Palaces.Select(x => x.Id).ToList();
        foreach (var room in rooms)
        {
            room.PalaceId = palaceIds[new Random().Next(0, palaceIds.Count)];
        }
        await _applicationDbContext.AddRangeAsync(rooms);
        await _applicationDbContext.SaveChangesAsync();
    }
}
