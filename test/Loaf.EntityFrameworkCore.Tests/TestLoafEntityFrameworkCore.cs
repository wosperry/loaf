
using Loaf.EntityFrameworkCore.Tests.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Loaf.EntityFrameworkCore.Tests.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Loaf.EntityFrameworkCore.Tests
{
    public class TestLoafEntityFrameworkCore : LoafEntityFrameworkCoreTestBase
    {
        public TestLoafEntityFrameworkCore()
        {
            using (var scope = Provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TestDbContext>();
                context.Database.Migrate(); //这里有了一次migrate，构造函数里的，所以不需要手动 update-database
            }
        }

        public override void ConfigureService(IServiceCollection services)
        {
            services.AddLoafDbContext<TestDbContext>((options,connString) => {
                options.UseSqlite(connString);
            });
        }

        [Theory(DisplayName ="新增")]
        [InlineData("张三","阿三","2022-01-01")]
        public void TestDbContextInsert(string name, string nickname, string birth)
        {
            using (var scope = Provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
                var students = dbContext.Set<Student>();

                Assert.NotNull(students);

                students!.Add(new Student
                {
                    Name = name,
                    NickName = nickname,
                    Birthday = DateTime.Parse(birth)
                });
                dbContext.SaveChanges();

                // 保存成功后，再查出来Assert一次
                var student = students.FirstOrDefault(t => t.Name == name);

                Assert.NotNull(student);

            }
        } 
    }

    /// <summary>
    /// Migrate的过程中，如果发现程序里有这个IDesignTimeDbContextFactory的实现，则不走代码里正常的步骤构建dbcontext
    /// 所以迁移不成功的时候可以手动实现这个工厂接口
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("TEST");

            var builder = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite(connectionString);

            return new TestDbContext(builder.Options);
        }
    }
}