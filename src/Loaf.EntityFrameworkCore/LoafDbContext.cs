using Loaf.EntityFrameworkCore.SoftDelete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loaf.EntityFrameworkCore
{
    public class LoafDbContext<TDbContext> : DbContext 
        where TDbContext: DbContext
    {
        public LoafDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IModelCustomizer, LoafModelCustomize>();
        }
    }
}