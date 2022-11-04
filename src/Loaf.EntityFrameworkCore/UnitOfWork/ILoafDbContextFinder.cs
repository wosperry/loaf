#nullable enable
using System;
using Loaf.EntityFrameworkCore.Core;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public interface ILoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    public DbContext GetDb();
}

public class LoafDbContextNotFoundException : Exception
{
    public LoafDbContextNotFoundException(string message):base(message)
    {
        
    }
}