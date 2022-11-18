using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Loaf.Core.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Modularity;

public static class LoafModuleHelper
{
    private static readonly List<LoafModule> Modules = new();

    public static IServiceCollection AddLoafModule<TModule>(this IServiceCollection services,
        IConfiguration configuration = null)
        where TModule : LoafModule
    {
        LoadModules(services, typeof(TModule));
        InvokeConfigureService(services, configuration);
        return services;
    }

    /// <summary>
    /// 调用模块的ConfigureService
    /// </summary>
    private static void InvokeConfigureService(IServiceCollection services, IConfiguration configuration = null)
    {
        foreach (var module in Modules)
        {
            module.PreConfigureService(new(services, configuration));
        }

        foreach (var module in Modules)
        {
            // TODO：加一个 LoafModuleOptions，AddLoafModule里加个Action，用于配置是否自动注册
            services.RegisterService(module.GetType().Assembly.GetTypes());
            
            module.ConfigureService(new(services, configuration));
        }

        foreach (var module in Modules)
        {
            module.PostConfigureService(new(services, configuration));
        }
    }

    /// <summary>
    /// Module 放到列表中备用
    /// </summary>
    private static IServiceCollection LoadModules(IServiceCollection services, Type moduleType)
    {
        var module = (Activator.CreateInstance(moduleType) as LoafModule)!;
        Modules.Add(module);

        if (moduleType.IsDefined(typeof(DependsOnAttribute), false))
        {
            var dependsAttribute = moduleType.GetCustomAttribute<DependsOnAttribute>()!;
            foreach (var dependModuleType in dependsAttribute.ModuleTypes)
            {
                LoadModules(services, dependModuleType);
            }
        }

        return services;
    }

    public static IApplicationBuilder Initialize(this IApplicationBuilder app)
    {
        foreach (var module in Modules)
        {
            module.PreInitialize(app);
        }
        foreach (var module in Modules)
        {
            module.Initialize(app);
        }
        foreach (var module in Modules)
        {
            module.PostInitialize(app);
        }

        return app;
    }
}