using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
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
        /// <param name="queryPropertyInfo">请求参数属性信息</param>
        /// <param name="entityPropertyInfo">TEntity的属性类型</param>
        /// <param name="value">值</param>
        /// <returns>拼接后表达式</returns>
        public BinaryExpression AndAlso<TEntity>(Expression originExpression, PropertyInfo queryPropertyInfo, object value)
        {
            // TEntity 属性表达式 
            var ex_t = Expression.Parameter(typeof(TEntity), "t");
            var entityPropertyName = !string.IsNullOrEmpty(PropertyName) ? PropertyName : queryPropertyInfo.Name;
            var entityPropertyInfo = typeof(TEntity).GetProperty(entityPropertyName);
            var propertyExpression = Expression.Property(ex_t, entityPropertyName);


            // 区分是否可空类型，可空则获取第一个泛型参数
            var destinyType = queryPropertyInfo.PropertyType.Name.Contains(nameof(Nullable))
                ? queryPropertyInfo.PropertyType.GetGenericArguments().First()
                : queryPropertyInfo.PropertyType;
            var valueExpression = Expression.Convert(Expression.Constant(value), destinyType);
            OnAppendingExpression(new() { Value = value, EntityPropertyInfo = entityPropertyInfo });
            return Expression.AndAlso(originExpression, GetCompareExpression(propertyExpression, valueExpression));
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
