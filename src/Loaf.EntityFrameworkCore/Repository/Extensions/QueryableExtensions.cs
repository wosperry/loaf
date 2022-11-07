#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Loaf.Core.Data;
using Loaf.Core.CommonExtensions;
using Loaf.EntityFrameworkCore.Repository.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.Repository.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// WhereIf
    /// </summary>
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> queryable, bool condition, Expression<Func<TEntity, bool>> predicate) where TEntity : class => condition ? queryable.Where(predicate) : queryable;


    public static async Task<PagedResult<TEntity>> GetPagedResultAsync<TEntity>(this IQueryable<TEntity> query, IPagination pagination, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var total = await query.CountAsync(cancellationToken);
        var entities = await query
            .PageBy(pagination)
            .ToListAsync(cancellationToken);
        return new(total, entities);
    }

    public static IQueryable<TEntity> PageBy<TEntity>(this IQueryable<TEntity> queryable, IPagination pagination)
    {
        return queryable
            .OrderBy(pagination.OrderBy)
            .Skip((pagination.Page - 1) * pagination.Size)
            .Take(pagination.Size);
    }

    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryable, string orderBy, bool isAsc = true)
    {
        var propInfo = typeof(TEntity).GetProperties().FirstOrDefault(t => t.Name.Equals(orderBy, StringComparison.OrdinalIgnoreCase));
        if (orderBy.IsNotEmpty() && propInfo is not null)
        {
            var methodName = isAsc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
            var method = typeof(Queryable).GetMethods().FirstOrDefault(method => method.Name == methodName && method.GetParameters().Count() == 2);
            var genericMethod = method!.MakeGenericMethod(typeof(TEntity), propInfo.PropertyType);

            var x = Expression.Parameter(typeof(TEntity), "x");
            var lambda = Expression.Lambda(Expression.Property(x, orderBy), x);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { queryable, lambda })!;
        }
        return queryable;
    }
    public static IQueryable<TEntity> BuildQueryLambdaByParameter<TEntity, TParameter>(this IQueryable<TEntity> query, TParameter parameter)
    {
        var ex_t = Expression.Parameter(typeof(TEntity), "t");

        Expression ex = Expression.Constant(true);

        foreach (var prop in typeof(TParameter).GetProperties())
        {
            var attrs = prop.GetCustomAttributes();
            var value = prop.GetValue(parameter);

            // 没有值的时候不查询
            if (value is null)
            {
                continue;
            }

            foreach (var attr in attrs)
            {
                // TODO: 添加更多判断的支持
                // TODO: 抽离到IEnumerable供普通迭代使用
                switch (attr)
                {
                    case LoafEqualsAttribute whereAttr:
                        ex = Expression.AndAlso(ex, Expression.Equal(
                            left: Expression.Property(ex_t, whereAttr.PropertyName.IsNotEmpty() ? whereAttr.PropertyName : prop.Name),
                            right: Expression.Convert(Expression.Constant(value), prop.PropertyType)));
                        break;
                    default:
                        break;
                }
            }
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(ex, ex_t);
        return query.Where(lambda);
    }
}
