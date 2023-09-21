using BokPalace.Domain.Rooms;
using BokPalace.Domain.ValueObjects;
using BokPalace.Infrastructure.Persistence.Comparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace BokPalace.Infrastructure.Persistence.Configurations;

public sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(36)
            .HasConversion(
            id => id.Value.ToString(),
            value => new RoomId(Guid.Parse(value)));

        var jsonConverterOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            IncludeFields = false,
        };

        builder.Property(x => x.Items)
            .HasConversion(
                list => JsonSerializer.Serialize(list, jsonConverterOptions),
                value => JsonSerializer.Deserialize<List<Item>>(value, jsonConverterOptions) ?? new List<Item>())
            .HasColumnType("TEXT")
            .Metadata.SetValueComparer(new CollectionComparer<Item>());


    }
}
