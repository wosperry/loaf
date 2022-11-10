using System;

namespace Loaf.Repository.Core.Attributes
{
    public abstract class LoafWhereAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}
