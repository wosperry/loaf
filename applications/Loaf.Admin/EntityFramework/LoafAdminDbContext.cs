using Loaf.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loaf.Admin.EntityFramework;

[ConnectionStringName("abc")]
public class LoafAdminDbContext:LoafDbContext<LoafAdminDbContext>
{
    public LoafAdminDbContext(DbContextOptions<LoafAdminDbContext> options) : base(options)
    {
    }
}