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

    /// <summary>
    /// 根据分页参数获取分页结果
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="query">quaryable</param>
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
                    // ex = ex.TryAndAlso<TEntity>(prop, whereAttr, value);
                    ex = whereAttr.AndAlso<TEntity>(ex, prop, value);
                }
            }
        }

        var ex_t = Expression.Parameter(typeof(TEntity), "t");
        var lambda = Expression.Lambda<Func<TEntity, bool>>(ex, ex_t);
        return query.Where(lambda);
    }

    /// <summary>
    /// 尝试拼接AndAlso到表达式ex
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="ex">原本的表达式</param>
    /// <param name="prop">查询参数Prop</param>
    /// <param name="whereAttr">查询参数标记的WhereAttribute</param>
    /// <param name="value">查询参数的值</param> 
    /// <returns>拼接后的表达式</returns>
    [Obsolete]
    private static Expression TryAndAlso<TEntity>(this Expression ex, PropertyInfo prop, LoafWhereAttribute whereAttr,object value)
    {
        var ex_t = Expression.Parameter(typeof(TEntity), "t");

        var propertyName = whereAttr.PropertyName.IsNotEmpty() ? whereAttr.PropertyName : prop.Name;
        var propertyExpression = Expression.Property(ex_t, propertyName);

        var valueExpression = Expression.Convert(Expression.Constant(value), prop.PropertyType.Name.Contains(nameof(Nullable))
            ? prop.PropertyType.GetGenericArguments().First()
            : prop.PropertyType);
        return whereAttr switch
        {
            LoafEqualsAttribute => Expression.AndAlso(ex, Expression.Equal(propertyExpression, valueExpression)),
            LoafGreaterThanAttribute => Expression.AndAlso(ex, Expression.GreaterThan(propertyExpression, valueExpression)),
            LoafGreaterThanOrEqualAttribute => Expression.AndAlso(ex, Expression.GreaterThanOrEqual(propertyExpression, valueExpression)),
            LoafLessThanAttribute => Expression.AndAlso(ex, Expression.LessThan(propertyExpression, valueExpression)),
            LoafLessThanOrEqualAttribute => Expression.AndAlso(ex, Expression.LessThanOrEqual(propertyExpression, valueExpression)),
            LoafStartWithAttribute => Expression.AndAlso(ex, Expression.Call(propertyExpression, typeof(string).GetMethod(nameof(string.StartsWith), new Type[] { typeof(string) })!, valueExpression)),
            LoafInAttribute containAttr =>
                prop.PropertyType.IsAssignableTo(typeof(System.Collections.ICollection)) ?
                    Expression.AndAlso(ex, Expression.Call(valueExpression, GetContainMethodInfo<TEntity>(prop, whereAttr.PropertyName.IsNotEmpty() ? whereAttr.PropertyName : prop.Name), propertyExpression)) :
                    Expression.AndAlso(ex, Expression.Call(propertyExpression, GetContainMethodInfo(prop), valueExpression)),
            _ => ex
        };
    }

    private static MethodInfo GetContainMethodInfo(PropertyInfo prop)
        => prop.PropertyType.GetMethod("Contains", new Type[] { prop.PropertyType }) ?? throw new Exception($"{nameof(LoafInAttribute)}特性只能标记在string或者List<>");

    private static MethodInfo GetContainMethodInfo<TEntity>(PropertyInfo prop, string propertyName)
    {
        var t = typeof(TEntity).GetProperty(propertyName)?.PropertyType ?? throw new Exception($"{nameof(LoafInAttribute)}特性只能标记在string或者List<>");
        return prop.PropertyType.GetMethod("Contains", new Type[] { t }) ?? throw new Exception($"{nameof(LoafInAttribute)}特性只能标记在string或者List<>");
    }
}
