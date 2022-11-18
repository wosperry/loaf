using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Loaf.EntityFrameworkCore;

/// <summary>
/// 替换为客制化Model创建，增加自己代码
/// 此方式实现，可以保证调用方不需要控制base.OnModelCreating的调用位置
/// </summary>
public class LoafModelCustomizer : ModelCustomizer
{
    public IServiceProvider Provider { get; }
    public LoafModelCustomizer(ModelCustomizerDependencies dependencies, IServiceProvider provider) : base(dependencies)
    {
        Provider = provider;
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        base.Customize(modelBuilder, context);
        modelBuilder.ApplyConfigurationsFromAssembly(context.GetType().Assembly);
        foreach (var customizer in Provider.GetServices<IModelCustomizing>())
        {
            customizer.Customize(modelBuilder, context);
        }
    }
}
