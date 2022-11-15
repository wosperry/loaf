using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loaf.Core.Data;
using Loaf.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore;

public static class QueryableExtensions
{
    /// <summary>
    /// 根据分页参数获取分页结果
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="query">过滤条件</param>
    /// <param name="pagination">分页参数，实现了分页接口的参数都行</param>
    /// <param name="cancellationToken">取消标识</param>
    /// <returns></returns>
    public static async Task<PagedResult<TEntity>> GetPagedResultAsync<TEntity>(this IQueryable<TEntity> query, IPagination pagination, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var total = await query.CountAsync(cancellationToken);
        var entities = await query
            .PageBy(pagination)
            .ToListAsync(cancellationToken);
        return new(total, entities);
    }
}