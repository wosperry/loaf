using System;
using System.Reflection;
using Loaf.Core.DependencyInjection;
using Loaf.EntityFrameworkCore.SoftDelete;
using Loaf.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore;

public static class EntityFrameworkCoreExtensions
{
    public static IServiceCollection AddLoafDbContext<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder, string> optionAction=null)
        where TDbContext : LoafDbContext<TDbContext>
    {
        return services
            .AddTransient<LoafSoftDeleteInterceptor>()
            .AddDbContext<TDbContext>((provider, options) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var connectionStringName = typeof(TDbContext)
                    .GetCustomAttribute<ConnectionStringNameAttribute>()
                    ?.ConnectionStringName ?? "Default";

                var connectionString = configuration.GetConnectionString(connectionStringName);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ConnectionStringNotSetException($"{typeof(TDbContext).Name}应设置 ConnectionStringNameAttribute，并提供与配置文件内配置的连接字符串相匹配的Key，若没有设置，则默认取 “Connectionstrings.Default”");
                }
                
                optionAction?.Invoke(options, connectionString);
                options.AddInterceptors(provider.GetService<LoafSoftDeleteInterceptor>()!);
            })
            .AddTransient(typeof(ILoafDbContextFinder<>),typeof(LoafDbContextFinder<>))
            .RegisterService(typeof(EntityFrameworkCoreExtensions).Assembly.GetTypes());
    }
}