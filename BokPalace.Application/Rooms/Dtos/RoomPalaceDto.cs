using BokPalace.Domain.Rooms;

namespace BokPalace.Application.Rooms.Dtos;

public class RoomPalaceDto
{
    public PalaceId Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
