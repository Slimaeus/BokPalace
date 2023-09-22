namespace BokPalace.Web.Data;

public sealed record CreateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
