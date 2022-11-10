using System;

namespace Loaf.Repository.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)] 
    public class LoafStartWithAttribute : LoafWhereAttribute { 
    }
}
