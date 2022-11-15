using System;
using System.Linq.Expressions;

namespace Loaf.Repository.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafLessThanOrEqualAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.LessThanOrEqual(propertyExpression, valueExpression);
        }
    }
}