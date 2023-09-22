namespace BokPalace.Web.Data;

public sealed record ItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}