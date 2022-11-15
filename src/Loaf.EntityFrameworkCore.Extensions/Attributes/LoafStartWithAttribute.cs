using System;
using System.Linq.Expressions;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafStartWithAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            var stringStartWith = typeof(string).GetMethod(nameof(string.StartsWith), new Type[] { typeof(string) });
            return Expression.Call(propertyExpression, stringStartWith, valueExpression);
        }
    }
}