using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BokPalace.Infrastructure.Persistence.Comparers;

public class CollectionComparer<T> : ValueComparer<ICollection<T>>
{
    public CollectionComparer()
        : base(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
            c => c.ToList())
    {
    }
}
