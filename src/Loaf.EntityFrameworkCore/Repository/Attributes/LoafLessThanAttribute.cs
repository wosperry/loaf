using System;
using System.Linq.Expressions;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafLessThanAttribute : LoafWhereAttribute
    {
        public override BinaryExpression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.LessThan(propertyExpression, valueExpression);
        }
    }
}
