using Loaf.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loaf.Admin.EntityFramework;

[ConnectionStringName("Default")]
public class LoafAdminDbContext:LoafDbContext<LoafAdminDbContext>
{
    public LoafAdminDbContext(DbContextOptions<LoafAdminDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}