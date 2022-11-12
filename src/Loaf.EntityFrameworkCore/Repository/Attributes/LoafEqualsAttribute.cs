using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class LoafEqualsAttribute : LoafWhereAttribute
    {
        public override BinaryExpression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.Equal(propertyExpression, valueExpression);
        }
    }
}
