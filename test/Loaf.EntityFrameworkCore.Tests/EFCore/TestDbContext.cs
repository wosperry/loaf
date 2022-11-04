using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
