using BokPalace.Domain.Items;
using BokPalace.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        if (await _applicationDbContext.Rooms.AnyAsync())
            return;

        var firstItemList = new Item[]
        {
            new Item
            {
                Name = "Ball",
                Description = "A ball"
            },
            new Item
            {
                Name = "Bowl",
                Description = "A bowl"
            },
            new Item
            {
                Name = "Apple",
                Description = "An apple"
            }
        };

        var secondItemList = new Item[]
        {
            new Item
            {
                Name = "Mirror",
                Description = "A mirror"
            }
        };

        var rooms = new Room[]
        {
            new Room
            {
                Name = "001",
                Description = "First room",
                Items = firstItemList.ToList()
            },
            new Room
            {
                Name = "002",
                Description = "Second room",
                Items = secondItemList.ToList()
            }
        };

        _applicationDbContext.Rooms
            .AddRange(rooms);

        //await _applicationDbContext.SaveChangesAsync();
    }
}
