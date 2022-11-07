using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.Repository.Attributes;

namespace Loaf.Admin.Controllers.Users
{
    public class UserQueryParameter : PageQueryParameter
    {
        //[LoafEquals(PropertyName ="Name")]
        public string? Name { get; set; }
        [LoafEquals]
        public string? Account { get; set; }
    }
}
