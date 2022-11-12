using System;
using System.Linq.Expressions;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafGreaterThanAttribute : LoafWhereAttribute
    {
        public override BinaryExpression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.GreaterThan(propertyExpression, valueExpression);
        }
    }
}
