using System.Reflection;

namespace Loaf.EntityFrameworkCore.Extensions.Attributes
{
    public class LoafExpressionAppendingContext
    {
        public object Value { get; set; }
        public PropertyInfo EntityPropertyInfo { get; internal set; }
    }
}