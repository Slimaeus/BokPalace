using BokPalace.Domain.Rooms;

namespace BokPalace.Application.Rooms.Dtos;

public sealed record RoomDto
{
    public RoomId Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();
};
