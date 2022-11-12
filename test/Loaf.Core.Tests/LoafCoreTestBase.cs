using Loaf.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Tests
{
    public abstract class LoafCoreTestBase
    {
        #region 属性、构造方法

        public IConfigurationRoot Configuration { get; }
        public ServiceProvider Provider { get; }

        public LoafCoreTestBase()
        {
            var services = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.RegisterService(typeof(DependencyInjectionTest).Assembly.GetTypes());

            Provider = services.BuildServiceProvider();
        }

        #endregion 属性、构造方法
    }
}