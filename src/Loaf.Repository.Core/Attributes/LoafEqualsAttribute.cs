using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Loaf.Repository.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class LoafEqualsAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            return Expression.Equal(propertyExpression, valueExpression);
        }
    }
}
