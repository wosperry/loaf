using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore;

public static class EntityFrameworkCoreExtensions
{
    public static IServiceCollection AddMyDbContext<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder, string> optionAction)
        where TDbContext : LoafDbContext
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
                    throw new ConnectionStringNotSetException($"{typeof(DbContext).Name}应设置 ConnectionStringNameAttribute，并提供与appsettings.json 内配置的连接字符串相匹配的Key，若没有设置，则默认取 Connectionstrings.Default");
                }

                options.AddInterceptors(provider.GetService<LoafSoftDeleteInterceptor>()!);

                optionAction?.Invoke(options, connectionString); 
            });
    }
}