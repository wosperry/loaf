namespace Loaf.Core.Data;

public abstract class Entity<TKey> : IEntity where TKey : struct
{
    public TKey Id { get; set; }
}