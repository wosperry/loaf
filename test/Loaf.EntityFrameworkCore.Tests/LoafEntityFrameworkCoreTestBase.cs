using Loaf.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore.Tests
{
    public abstract class LoafEntityFrameworkCoreTestBase
    {
        #region 属性、构造方法

        public IConfiguration Configuration { get; }
        public ServiceProvider Provider { get; }

        public LoafEntityFrameworkCoreTestBase()
        {
            var services = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton(Configuration);
            services.RegisterService(typeof(LoafEntityFrameworkCoreTestBase).Assembly.GetTypes());
            ConfigureService(services);
            Provider = services.BuildServiceProvider();
        }

        #endregion 属性、构造方法

        public virtual void ConfigureService(IServiceCollection services)
        {
        }
    }
}