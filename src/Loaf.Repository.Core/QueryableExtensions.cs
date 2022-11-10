using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection; 
using Loaf.Core.Data;
using Loaf.Core.CommonExtensions;
using Loaf.Repository.Core.Attributes;

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
                if (attr is LoafWhereAttribute whereAttr)
                {
                    var propertyName = whereAttr.PropertyName.IsNotEmpty() ? whereAttr.PropertyName : prop.Name;
                    var propertyExpression = Expression.Property(ex_t, propertyName);

                    var valueExpression = Expression.Convert(Expression.Constant(value), prop.PropertyType.Name.Contains(nameof(Nullable))
                        ? prop.PropertyType.GetGenericArguments().First()
                        : prop.PropertyType);

                    ex = whereAttr switch
                    {
                        LoafEqualsAttribute => Expression.AndAlso(ex, Expression.Equal(propertyExpression, valueExpression)),
                        LoafGreaterThanAttribute => Expression.AndAlso(ex, Expression.GreaterThan(propertyExpression, valueExpression)),
                        LoafGreaterThanOrEqualAttribute => Expression.AndAlso(ex, Expression.GreaterThanOrEqual(propertyExpression, valueExpression)),
                        LoafLessThanAttribute => Expression.AndAlso(ex, Expression.LessThan(propertyExpression, valueExpression)),
                        LoafLessThanOrEqualAttribute => Expression.AndAlso(ex, Expression.LessThanOrEqual(propertyExpression, valueExpression)),
                        LoafStartWithAttribute => Expression.AndAlso(ex, Expression.Call(propertyExpression, typeof(string).GetMethod(nameof(string.StartsWith), new Type[] { typeof(string) })!, valueExpression)),
                        LoafContainsAttribute containAttr =>
                            typeof(System.Collections.ICollection).IsAssignableFrom(prop.PropertyType) ?
                                Expression.AndAlso(ex, Expression.Call(valueExpression, GetContainMethodInfo<TEntity>(prop, propertyName), propertyExpression)) :
                                Expression.AndAlso(ex, Expression.Call(propertyExpression, GetContainMethodInfo(prop), valueExpression)),
                        _ => ex
                    };
                }
            }
        }

        var lambda = Expression.Lambda<Func<TEntity, bool>>(ex, ex_t);
        return query.Where(lambda);
    }

    private static MethodInfo GetContainMethodInfo(PropertyInfo prop)
        => prop.PropertyType.GetMethod("Contains", new Type[] { prop.PropertyType }) ?? throw new Exception($"{nameof(LoafContainsAttribute)}特性只能标记在string或者List<>");

    private static MethodInfo GetContainMethodInfo<TEntity>(PropertyInfo prop, string propertyName)
    {
        var t = typeof(TEntity).GetProperty(propertyName)?.PropertyType ?? throw new Exception($"{nameof(LoafContainsAttribute)}特性只能标记在string或者List<>");
        return prop.PropertyType.GetMethod("Contains", new Type[] { t }) ?? throw new Exception($"{nameof(LoafContainsAttribute)}特性只能标记在string或者List<>");
    }
}
