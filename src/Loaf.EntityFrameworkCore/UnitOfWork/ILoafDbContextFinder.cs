#nullable enable
using System;
using Loaf.EntityFrameworkCore.Core;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public interface ILoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    public LoafDbContext GetDb();
}

public class LoafDbContextNotFoundException : Exception
{
    public LoafDbContextNotFoundException(string message):base(message)
    {
        
    }
}