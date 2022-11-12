﻿using System;
using System.Linq.Expressions;
using Loaf.Repository.Core.Attributes;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafLeftLikeAttribute : LoafWhereAttribute
    {
        public override Expression GetCompareExpression(Expression propertyExpression, Expression valueExpression)
        {
            var stringStartWith = typeof(string).GetMethod(nameof(string.StartsWith), new Type[] { typeof(string) });
            return Expression.Call(propertyExpression, stringStartWith, valueExpression);
        }
    }
}