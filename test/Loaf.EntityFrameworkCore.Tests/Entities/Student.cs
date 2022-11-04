#pragma warning disable CS8618

namespace Loaf.EntityFrameworkCore.Tests.Entities
{
    public class Student : Entity<Guid>
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
