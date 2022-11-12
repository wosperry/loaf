using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore
{
    public class LoafDbContext<TDbContext> : DbContext
        where TDbContext : DbContext
    {
        public LoafDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }
    }
}