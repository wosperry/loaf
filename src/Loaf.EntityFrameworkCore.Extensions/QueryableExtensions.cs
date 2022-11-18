using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Loaf.Core.CommonExtensions;
using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.Extensions.Attributes;

namespace Loaf.Repository.Core;

public static class QueryableCommonExtensions
{
    /// <summary>
    /// WhereIf
    /// </summary>
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> queryable, bool condition, Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return condition ? queryable.Where(predicate) : queryable;
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
        Expression ex = Expression.Constant(true);

        var ex_t = Expression.Parameter(typeof(TEntity), "t");

        foreach (var prop in typeof(TParameter).GetProperties())
        { 
            foreach (var attr in prop.GetCustomAttributes())
            {
                if (attr is LoafWhereAttribute whereAttr)
                {
                    ex = whereAttr.AndAlso<TEntity>(ex, ex_t, prop, prop.GetValue(parameter));
                }
            }
        }

        return query.Where(Expression.Lambda<Func<TEntity, bool>>(ex, ex_t));
    }
}