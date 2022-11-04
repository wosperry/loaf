#nullable enable
using System.Collections.Generic;
using System.Linq;
using Loaf.Core.DependencyInjection;
using Loaf.EntityFrameworkCore.Core;
using Loaf.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public class LoafDbContextFinder<TEntity>: ILoafDbContextFinder<TEntity>, ITransient
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

    public LoafDbContext GetDb() => _loafDbContext;
}