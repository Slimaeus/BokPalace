using BokPalace.Domain.Palaces;
using BokPalace.Domain.Primitives;
using BokPalace.Domain.ValueObjects;

namespace BokPalace.Domain.Rooms;

public sealed class Room : BaseEntity<RoomId>
{
    public Room() : base(new RoomId(Guid.NewGuid())) { }
    public Room(RoomId id) : base(id)
    {
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public PalaceId? PalaceId { get; set; }
    public Palace? Palace { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
