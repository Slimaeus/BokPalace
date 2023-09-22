namespace BokPalace.Web.Data;

public sealed record RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    //public RoomPalaceDto? Palace { get; set; }
    public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();
}
