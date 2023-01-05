using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore.SoftDelete;

public class LoafSoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly IServiceProvider _provider;

    public LoafSoftDeleteInterceptor(IServiceProvider provider)
    {
        _provider = provider;
    }
     
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (var trackingEntry in eventData.Context.ChangeTracker.Entries())
        {
            if (trackingEntry.State != EntityState.Deleted) 
                continue;
            if (trackingEntry.Entity is ISoftDelete entity)
            {
                // 假删除
                entity.IsDeleted = true;
                trackingEntry.State = EntityState.Modified;

                var handler = _provider.GetService<ISoftDeleteHandler>();
                if (handler is not null)
                {
                    var methodInfo = typeof(ISoftDeleteHandler).GetMethod(nameof(ISoftDeleteHandler.SoftDeleteExecuting))?
                        .MakeGenericMethod(trackingEntry.Entity.GetType());
                    methodInfo?.Invoke(handler, new object[] { trackingEntry.Entity });
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}