using System.Reflection;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    public class LoafExpressionAppendingContext
    {
        public object Value { get; set; }
        public PropertyInfo EntityPropertyInfo { get; internal set; }
    }
}