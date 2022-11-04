using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Loaf.Core.DependencyInjection;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// 根据接口自动注册
    /// </summary>
    public static IServiceCollection RegisterService(this IServiceCollection services, params Type[] serviceTypes)
    {
        var types = serviceTypes.Where(t => t.IsPublic && typeof(IDependencyInjection).IsAssignableFrom(t))
            .ToList();
        services.RegisterService<ITransient>(ServiceLifetime.Transient,types);
        services.RegisterService<IScoped>(ServiceLifetime.Scoped, types);
        services.RegisterService<ISingleton>(ServiceLifetime.Singleton, types);
        return services;
    }

    private static IServiceCollection RegisterService<T>(this IServiceCollection services, ServiceLifetime lifetime, IEnumerable<Type> types)
        where T: IDependencyInjection
    {
        foreach (var serviceType in types.Where(t=>typeof(T).IsAssignableFrom(t) && !t.IsAbstract))
        {
            // ReSharper disable once CommentTypo
            // 忽略掉 IDisposible，与ITransient、ISingleton等接口
            var interfaces = serviceType.GetInterfaces().Where(i => 
                !typeof(T).IsAssignableFrom(i) &&
                !typeof(IDisposable).IsAssignableFrom(i));
            services.Add(ServiceDescriptor.Describe(serviceType, serviceType, lifetime));
            services.Add(interfaces.Select(@interface => ServiceDescriptor.Describe(@interface, serviceType,lifetime)));
        }
        return services;
    }
}