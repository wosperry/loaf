﻿using System;
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

    public static IServiceCollection AddModule<TModule>(this IServiceCollection services,
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
        foreach (var preConfigureService in Modules.Select(m => (Action<LoafModuleContext>)m.PreConfigureService))
        {
            preConfigureService(new(services, configuration));
        }

        foreach (var module in Modules)
        {
            // TODO：加一个 LoafModuleOptions，AddLoafModule里加个Action，用于配置是否自动注册
            services.RegisterService(module.GetType().Assembly.GetTypes());
            module.ConfigureService(new(services, configuration));
        }

        foreach (var postConfigureService in Modules.Select(m => (Action<LoafModuleContext>)m.PostConfigureService))
        {
            postConfigureService(new(services, configuration));
        }
    }

    /// <summary>
    /// Module 放到列表中备用
    /// </summary>
    private static IServiceCollection LoadModules(IServiceCollection services, Type moduleType)
    {
        var module = (Activator.CreateInstance(moduleType) as LoafModule)!;
        Modules.Add(module);

        if (moduleType.IsDefined(typeof(AddModuleAttribute), false))
        {
            var dependsAttribute = moduleType.GetCustomAttribute<AddModuleAttribute>()!;
            foreach (var dependModuleType in dependsAttribute.ModuleTypes)
            {
                LoadModules(services, dependModuleType);
            }
        }

        return services;
    }

    public static IApplicationBuilder Initialize(this IApplicationBuilder app)
    {
        foreach (var action in Modules.Select(m => (Action<IApplicationBuilder>)m.PreInitialize))
        {
            action(app);
        }

        foreach (var action in Modules.Select(m => (Action<IApplicationBuilder>)m.Initialize))
        {
            action(app);
        }

        foreach (var action in Modules.Select(m => (Action<IApplicationBuilder>)m.PostInitialize))
        {
            action(app);
        }

        return app;
    }
}