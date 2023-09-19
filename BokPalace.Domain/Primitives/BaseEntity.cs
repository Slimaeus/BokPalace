using BokPalace.Domain.Interfaces;

namespace BokPalace.Domain.Primitives;

public abstract class BaseEntity<T> : IEquatable<BaseEntity<T>>, IEntity<T> where T : notnull
{
    public T Id { get; set; }
    protected BaseEntity(T id) => Id = id;
    public static bool operator ==(BaseEntity<T>? first, BaseEntity<T>? second)
    {
        return first is { } && second is { } && first.Equals(second);
    }
    public static bool operator !=(BaseEntity<T>? first, BaseEntity<T>? second)
    {
        return !(first == second);
    }
    public bool Equals(BaseEntity<T>? other)
    {
        if (other is null
            || other.GetType() != GetType())
            return false;
        return other.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null
            || obj.GetType() != GetType()
            || obj is not BaseEntity<T> entity)
            return false;
        return entity.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
