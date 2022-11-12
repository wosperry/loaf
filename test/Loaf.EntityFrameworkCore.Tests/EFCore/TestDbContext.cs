using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.Tests.EFCore
{
    [ConnectionStringName("TEST")]
    public class TestDbContext : LoafDbContext<TestDbContext>
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
    }
}