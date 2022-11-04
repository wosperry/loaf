using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore;

public class LoafSoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly IServiceProvider _provider;

    public LoafSoftDeleteInterceptor(IServiceProvider provider)
    {
        _provider = provider;
    }
 
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context?.ChangeTracker.Entries().Any() ?? false)
        {
            foreach (var trackingEntry in eventData.Context.ChangeTracker.Entries())
            {
                if (trackingEntry.State == EntityState.Deleted)
                {

                    if (trackingEntry.Entity is ISoftDelete entity)
                    {
                        entity.IsDeleted = true;
                    }
                    var handler = _provider.GetService<ISoftDeleteHandler>();
                    var methodInfo = typeof(ISoftDeleteHandler).GetMethod(nameof(ISoftDeleteHandler.SoftDeleteExecuting))?
                        .MakeGenericMethod(trackingEntry.Entity.GetType());

                    if (handler is not null)
                    {
                        methodInfo?.Invoke(handler, new object[] { trackingEntry.Entity });
                    }

                    trackingEntry.State = EntityState.Modified;
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}