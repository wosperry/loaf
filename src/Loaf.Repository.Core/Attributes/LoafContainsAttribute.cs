using System;
using Loaf.Repository.Core.Attributes;

namespace Loaf.Repository.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LoafContainsAttribute : LoafWhereAttribute
    {
    }
}
