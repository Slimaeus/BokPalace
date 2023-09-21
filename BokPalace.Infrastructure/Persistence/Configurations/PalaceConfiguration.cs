using BokPalace.Domain.Palaces;
using BokPalace.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BokPalace.Infrastructure.Persistence.Configurations;

public class PalaceConfiguration : IEntityTypeConfiguration<Palace>
{
    public void Configure(EntityTypeBuilder<Palace> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(36)
            .HasConversion(
            id => id.Value.ToString(),
            value => new PalaceId(Guid.Parse(value)));
    }
}
