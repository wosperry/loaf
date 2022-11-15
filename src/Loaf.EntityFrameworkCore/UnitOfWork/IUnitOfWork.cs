using System.Threading;
using System.Threading.Tasks;

namespace Loaf.EntityFrameworkCore.UnitOfWork;

public interface IUnitOfWork
{
    Task BeginAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollBackAsync(CancellationToken cancellationToken = default);
}