using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IEnumerable<DbContext> _contexts;
    private List<IDbContextTransaction> Transactions { get; } = new();

    public UnitOfWork(IEnumerable<DbContext> contexts)
    {
        _contexts = contexts;
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task BeginAsync(CancellationToken cancellationToken)
    {
        foreach (var context in _contexts)
        {
            Transactions.Add(await context.Database.BeginTransactionAsync(cancellationToken));
        }
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task RollBackAsync(CancellationToken cancellationToken)
    {
        foreach (var transaction in Transactions)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        foreach (var transaction in Transactions)
        {
            await transaction.CommitAsync(cancellationToken);
        }
    }
}