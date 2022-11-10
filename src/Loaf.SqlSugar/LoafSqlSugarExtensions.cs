using System;
using System.Collections.Generic;
using System.Text;
using Loaf.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar.IOC;

namespace Loaf.SqlSugar
{
    public static class LoafSqlSugarExtensions
    {
        public static IServiceCollection AddLoafSqlSugar(this IServiceCollection services, Action<IocConfig> options)
        {
            var iocConfig = new IocConfig();
            options?.Invoke(iocConfig);
            services.AddSqlSugar(iocConfig);


            return services;
        }
    }

    public class LoafSqlSugarModule:LoafModule
    {
        public override void ConfigureService(LoafModuleContext context)
        {
            context.Services.AddLoafSqlSugar(options =>
            {
                context.Configuration.GetSection("SqlSugarOptions").Bind(options);
            });
        }
    }
}
