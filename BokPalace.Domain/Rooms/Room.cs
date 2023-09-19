using BokPalace.Domain.Items;
using BokPalace.Domain.Primitives;

namespace BokPalace.Domain.Rooms;

public sealed class Room : BaseEntity<RoomId>
{
    public Room() : base(new RoomId(Guid.NewGuid())) { }
    public Room(RoomId id) : base(id)
    {
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
