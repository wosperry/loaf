using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loaf.EntityFrameworkCore.Repository.Attributes
{
    public abstract class LoafWhereAttribute: Attribute { }
    public class LoafEqualsAttribute: LoafWhereAttribute
    {
        public string PropertyName { get; set; } 
    }
}
