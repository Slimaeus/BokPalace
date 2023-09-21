using BokPalace.Application.Rooms.Dtos;
using BokPalace.Domain.Rooms;

namespace BokPalace.Application.Palaces.Dtos;

public sealed record PalaceDto
{
    public PalaceId Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<PalaceRoomDto> Rooms { get; set; } = new List<PalaceRoomDto>();
};
