using System;
using System.Linq.Expressions;

namespace Loaf.Repository.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafGreaterThanOrEqualAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.GreaterThanOrEqual(propertyExpression, valueExpression);
        }
    }
}
