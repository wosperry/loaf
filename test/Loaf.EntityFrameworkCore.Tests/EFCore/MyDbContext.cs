using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.Tests.EFCore
{
    public class MyDbContext : LoafDbContext<MyDbContext>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
