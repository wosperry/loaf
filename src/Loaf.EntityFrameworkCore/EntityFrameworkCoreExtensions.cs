using System;
using System.Reflection;
using Loaf.Core.Data;
using Loaf.Core.DependencyInjection;
using Loaf.EntityFrameworkCore.Repository;
using Loaf.EntityFrameworkCore.SoftDelete;
using Loaf.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore;

public static class EntityFrameworkCoreExtensions
{
    public static IServiceCollection AddLoafDbContext<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder, string> optionAction = null)
        where TDbContext : DbContext
    {
        return services
            .AddTransient<LoafSoftDeleteInterceptor>()
            .AddTransient<DbContext, TDbContext>()
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
                options.ReplaceService<IModelCustomizer, LoafModelCustomize>();
            })
            .AddTransient(typeof(ILoafDbContextFinder<>), typeof(LoafDbContextFinder<>))
            .AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>))
            .RegisterService(typeof(EntityFrameworkCoreExtensions).Assembly.GetTypes());
    }
}