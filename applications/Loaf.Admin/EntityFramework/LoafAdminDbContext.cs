using Loaf.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loaf.Admin.EntityFramework;

public class LoafAdminDbContext:LoafDbContext
{
    public LoafAdminDbContext(DbContextOptions<LoafAdminDbContext> options) : base(options)
    {
    }
}