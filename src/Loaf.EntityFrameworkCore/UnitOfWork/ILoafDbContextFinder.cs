#nullable enable

using System;
using Loaf.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public interface ILoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    public DbContext GetCurrentDbContext();
}

public class LoafDbContextNotFoundException : Exception
{
    public LoafDbContextNotFoundException(string message) : base(message)
    {
    }
}