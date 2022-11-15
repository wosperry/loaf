using System;
using System.Linq.Expressions;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafGreaterThanAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.GreaterThan(propertyExpression, valueExpression);
        }
    }
}