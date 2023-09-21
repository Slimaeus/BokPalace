using BokPalace.Domain.Primitives;

namespace BokPalace.Domain.ValueObjects;

public sealed class Item : ValueObject
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ToString();
    }
}
