using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafInAttribute : LoafWhereAttribute
    {
        private MethodInfo _method;

        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.Call(valueExpression, _method, propertyExpression); 
        }

        public override void OnAppendingExpression(LoafExpressionAppendingContext context)
        {
            if (context.Value is IEnumerable)
            {
                _method = context.Value.GetType().GetMethod("Contains", new Type[] { context.EntityPropertyInfo.PropertyType });
            }
            else
            {
                throw new Exception($"{nameof(LoafInAttribute)}特性只能标记在List<>");
            }
        } 
    }
}