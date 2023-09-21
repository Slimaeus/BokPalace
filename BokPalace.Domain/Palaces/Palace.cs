using BokPalace.Domain.Primitives;
using BokPalace.Domain.Rooms;

namespace BokPalace.Domain.Palaces;

public sealed class Palace : BaseEntity<PalaceId>
{
    public Palace() : base(new PalaceId(Guid.NewGuid())) { }
    public Palace(PalaceId id) : base(id)
    {
    }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
