using System;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    public abstract class LoafWhereAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}
