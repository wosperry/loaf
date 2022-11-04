using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loaf.EntityFrameworkCore.Core
{
    public class LoafDbContext: DbContext
    {
        public LoafDbContext(DbContextOptions options) : base(options)
        {

        }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IModelCustomizer, LoafModelCustomize>();
        }
    }
}