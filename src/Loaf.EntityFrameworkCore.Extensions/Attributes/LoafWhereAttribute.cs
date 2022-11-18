using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    public abstract class LoafWhereAttribute : Attribute
    {
        /// <summary>
        /// 实体Property名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 拼接AndAlso
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="originExpression">原始表达式</param>
        /// <param name="ex_t">t=>t.Name 中的t</param>
        /// <param name="queryPropertyInfo">请求参数属性信息</param>
        /// <param name="value">值</param>
        /// <returns>拼接后表达式</returns>
        public Expression AndAlso<TEntity>(Expression originExpression, ParameterExpression ex_t, PropertyInfo queryPropertyInfo, object value)
        {
            if (value is null)
                return originExpression;

            var (propertyExpression, valueExpression) = GetExpressions<TEntity>(originExpression, ex_t, queryPropertyInfo, value);
            return Expression.AndAlso(originExpression, GetCompareExpression(propertyExpression, valueExpression));
        }

        private (Expression propertyExpression, Expression valueExpression) GetExpressions<TEntity>(Expression originExpression, ParameterExpression ex_t, PropertyInfo queryPropertyInfo, object value)
        {
            var entityPropertyName = !string.IsNullOrEmpty(PropertyName) ? PropertyName : queryPropertyInfo.Name;
            var entityPropertyInfo = typeof(TEntity).GetProperty(entityPropertyName);
            var propertyExpression = Expression.Property(ex_t, entityPropertyName);
            // 区分是否可空类型，可空则获取第一个泛型参数
            var destinyType = queryPropertyInfo.PropertyType.Name.Contains(nameof(Nullable))
                ? queryPropertyInfo.PropertyType.GetGenericArguments().First()
                : queryPropertyInfo.PropertyType;
            var valueExpression = Expression.Convert(Expression.Constant(value), destinyType);

            OnAppendingExpression(new() { Value = value, EntityPropertyInfo = entityPropertyInfo });
            return (propertyExpression, valueExpression);
        }

        /// <summary>
        /// 获取比较表达式
        /// </summary>
        public abstract Expression GetCompareExpression(Expression left, Expression right);

        public virtual void OnAppendingExpression(LoafExpressionAppendingContext context)
        {
        }
    }
}