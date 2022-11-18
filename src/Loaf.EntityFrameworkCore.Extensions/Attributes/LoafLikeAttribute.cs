using System;
using System.Linq.Expressions;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafLikeAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            var stringContains = typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) });
            return Expression.Call(propertyExpression, stringContains, valueExpression);
        }
    }
}