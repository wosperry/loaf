using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Loaf.Core.DependencyInjection;
using Loaf.EntityFrameworkCore.Tests.EFCore;

namespace Loaf.EntityFrameworkCore.Tests
{
    public abstract class LoafEntityFrameworkCoreTestBase
    {
        #region 属性、构造方法
        // ReSharper disable once MemberCanBePrivate.Global
        public IConfiguration Configuration { get; }
        // ReSharper disable once MemberCanBeProtected.Global
        public ServiceProvider Provider { get; }
        // ReSharper disable once PublicConstructorInAbstractClass
        public LoafEntityFrameworkCoreTestBase()
        {
            var services = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton(Configuration);
            services.RegisterService(typeof(LoafEntityFrameworkCoreTestBase).Assembly.GetTypes());
            // ReSharper disable once VirtualMemberCallInConstructor
            ConfigureService(services);
            Provider = services.BuildServiceProvider();
        }
        #endregion

        // ReSharper disable once MemberCanBeProtected.Global
        protected virtual void ConfigureService(IServiceCollection services)
        {

        }
    }
}