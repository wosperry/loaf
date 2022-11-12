using Loaf.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Tests
{
    /// <summary>
    /// 测试标记注入接口自动注册到容器
    /// </summary>

    public class DependencyInjectionTest : LoafCoreTestBase
    {
        [Fact(DisplayName = "注入接口类")]
        public void TestInjectServiceType()
        {
            var greetingService1 = Provider.GetService<ITransientGreetingService>();
            Assert.NotNull(greetingService1);
        }

        [Fact(DisplayName = "注入实现类")]
        public void TestInjectImplementType()
        {
            var greetingService1 = Provider.GetService<TransientGreetingService>();
            Assert.NotNull(greetingService1);
        }

        [Fact(DisplayName = "瞬态")]
        public void TestTransient()
        {
            var greetingService1 = Provider.GetService<ITransientGreetingService>();
            var greetingService2 = Provider.GetService<ITransientGreetingService>();
            Assert.NotNull(greetingService1);
            Assert.NotNull(greetingService2);
            Assert.NotEqual(greetingService1!.Id, greetingService2!.Id);
        }

        [Fact(DisplayName = "作用域")]
        public void TestScoped()
        {
            IScopedGreetingService? greetingService1, greetingService2;
            using (var scope = Provider.CreateScope())
            {
                greetingService1 = scope.ServiceProvider.GetService<IScopedGreetingService>();
                greetingService2 = scope.ServiceProvider.GetService<IScopedGreetingService>();
                Assert.NotNull(greetingService1);
                Assert.NotNull(greetingService2);
                Assert.Equal(greetingService1!.Id, greetingService2!.Id);

                using (var scope1 = Provider.CreateScope())
                {
                    greetingService2 = scope1.ServiceProvider.GetService<IScopedGreetingService>();
                    Assert.NotNull(greetingService2);
                    Assert.NotEqual(greetingService1!.Id, greetingService2!.Id);
                }
            }
        }

        [Fact(DisplayName = "单例")]
        public void TestSingleton()
        {
            ISingletonGreetingService? singletonGreetingService1, singletonGreetingService2, singletonGreetingService3;

            singletonGreetingService1 = Provider.GetService<ISingletonGreetingService>();
            singletonGreetingService2 = Provider.GetService<ISingletonGreetingService>();

            Assert.NotNull(singletonGreetingService1);
            Assert.NotNull(singletonGreetingService2);

            Assert.Equal(singletonGreetingService1!.Id, singletonGreetingService2!.Id);

            using (var scope = Provider.CreateScope())
            {
                singletonGreetingService3 = scope.ServiceProvider.GetService<ISingletonGreetingService>();
                Assert.NotNull(singletonGreetingService3);

                Assert.Equal(singletonGreetingService1.Id, singletonGreetingService3!.Id);
                Assert.Equal(singletonGreetingService2.Id, singletonGreetingService3!.Id);
            }
        }
    }

    public interface ITransientGreetingService
    {
        public Guid Id { get; set; }

        string Hello();
    }

    public class TransientGreetingService : ITransientGreetingService, ITransient
    {
        public TransientGreetingService()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Hello()
        {
            return "Hello";
        }
    }

    public interface IScopedGreetingService
    {
        public Guid Id { get; set; }

        string Hello();
    }

    public class ScopedGreetingService : IScopedGreetingService, IScoped
    {
        public ScopedGreetingService()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Hello()
        {
            return "Hello";
        }
    }

    public interface ISingletonGreetingService
    {
        public Guid Id { get; set; }

        string Hello();
    }

    public class SingletonGreetingService : ISingletonGreetingService, ISingleton
    {
        public SingletonGreetingService()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Hello()
        {
            return "Hello";
        }
    }
}