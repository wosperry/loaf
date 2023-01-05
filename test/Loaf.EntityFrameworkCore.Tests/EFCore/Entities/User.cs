using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.SoftDelete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaf.EntityFrameworkCore.Tests.EFCore.Entities
{
    public class User: Entity<Guid>,ISoftDelete
    {
#pragma warning disable CS8618
        private User() { }
#pragma warning restore CS8618 
        public User(string name, string email, string userName, string password)
        {
            Name = name;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
    }
}
