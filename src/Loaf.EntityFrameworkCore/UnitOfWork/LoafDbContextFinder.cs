#nullable enable

using System.Collections.Generic;
using System.Linq;
using Loaf.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public class LoafDbContextFinder<TEntity> : ILoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbContext _loafDbContext;

    public LoafDbContextFinder(IEnumerable<DbContext> dbContexts)
    {
        var context = dbContexts.FirstOrDefault(context =>
            context.Model.GetEntityTypes().Any(entityType => entityType.ClrType == typeof(TEntity)));
        if (context is DbContext loafDbContext)
        {
            _loafDbContext = loafDbContext;
        }
        else
        {
            throw new LoafDbContextNotFoundException("未找到继承自LoafDbContext的数据库上下文");
        }
    }

    public DbContext GetCurrentDbContext() => _loafDbContext;
}