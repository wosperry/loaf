#nullable enable
using System.Collections.Generic;
using System.Linq;
using Loaf.EntityFrameworkCore.Core;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.Repository;

public class LoafDbContextFinder<TEntity>
    where TEntity : class, IEntity
{
    private readonly LoafDbContext _loafDbContext;

    public LoafDbContextFinder(IEnumerable<DbContext> dbContexts)
    {
        var context = dbContexts.FirstOrDefault(context =>
            context.Model.GetEntityTypes().Any(entityType => entityType.ClrType == typeof(TEntity))) ;
        if (context is LoafDbContext loafDbContext)
        {
            _loafDbContext = loafDbContext;
        }
        else
        {
            throw new LoafDbContextNotFoundException("未找到继承自LoafDbContext的数据库上下文");
        }
    }

    public LoafDbContext GetDbContxt() => _loafDbContext;
}