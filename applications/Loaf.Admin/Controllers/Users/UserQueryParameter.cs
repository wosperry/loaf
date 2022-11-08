using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.Repository.Attributes;

namespace Loaf.Admin.Controllers.Users
{
    public class UserQueryParameter : PageQueryParameter
    {
        [LoafContains]
        public string? Name { get; set; }
        [LoafEquals]
        public string? Account { get; set; }

        [LoafContains(PropertyName = "Name")]
        public List<string>? TestNames { get; set; }
    }
}
