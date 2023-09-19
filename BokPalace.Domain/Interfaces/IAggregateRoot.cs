namespace BokPalace.Domain.Interfaces;
public interface IAggregateRoot<out T> : IEntity<T> where T : notnull
{
}