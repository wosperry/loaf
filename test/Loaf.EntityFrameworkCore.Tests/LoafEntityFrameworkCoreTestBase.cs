using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Loaf.Core.DependencyInjection;
using Loaf.EntityFrameworkCore.Tests.EFCore;

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
        #endregion

        public virtual void ConfigureService(IServiceCollection services)
        {

        }
    }
}