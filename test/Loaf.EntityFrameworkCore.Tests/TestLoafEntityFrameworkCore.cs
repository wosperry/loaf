
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
                context.Database.Migrate(); //��������һ��migrate�����캯����ģ����Բ���Ҫ�ֶ� update-database
            }
        }

        public override void ConfigureService(IServiceCollection services)
        {
            services.AddLoafDbContext<TestDbContext>((options,connString) => {
                options.UseSqlite(connString);
            });
        }

        [Theory(DisplayName ="����")]
        [InlineData("����","����","2022-01-01")]
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

                // ����ɹ����ٲ����Assertһ��
                var student = students.FirstOrDefault(t => t.Name == name);

                Assert.NotNull(student);

            }
        } 
    }

    /// <summary>
    /// Migrate�Ĺ����У�������ֳ����������IDesignTimeDbContextFactory��ʵ�֣����ߴ����������Ĳ��蹹��dbcontext
    /// ����Ǩ�Ʋ��ɹ���ʱ������ֶ�ʵ����������ӿ�
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