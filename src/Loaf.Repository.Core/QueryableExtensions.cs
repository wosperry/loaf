using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection; 
using Loaf.Core.Data;
using Loaf.Core.CommonExtensions;
using Loaf.Repository.Core.Attributes;
using Loaf.EntityFrameworkCore.Repository.Attributes;

namespace Loaf.Repository.Core;

public static class QueryableCommonExtensions
{
    /// <summary>
    /// WhereIf
    /// </summary>
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> queryable, bool condition, Expression<Func<TEntity, bool>> predicate) where TEntity : class => condition ? queryable.Where(predicate) : queryable;

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
                if (attr is LoafWhereAttribute whereAttr)
                {
                    ex = whereAttr.AndAlso<TEntity>(ex, prop, value);
                }
            }
        }

        var ex_t = Expression.Parameter(typeof(TEntity), "t");
        var lambda = Expression.Lambda<Func<TEntity, bool>>(ex, ex_t);
        return query.Where(lambda);
    }
}
