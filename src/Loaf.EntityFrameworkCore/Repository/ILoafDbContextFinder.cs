#nullable enable
using System;
using Loaf.EntityFrameworkCore.Core;

namespace Loaf.EntityFrameworkCore.Repository;

public interface ILoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    public LoafDbContext Find();
}

public class LoafDbContextNotFoundException : Exception
{
    public LoafDbContextNotFoundException(string message):base(message)
    {
        
    }
}