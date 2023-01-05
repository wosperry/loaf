using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loaf.EntityFrameworkCore;

/// <summary>
/// 替换为客制化Model创建，增加自己代码
/// 此方式实现，可以保证调用方不需要控制base.OnModelCreating的调用位置
/// </summary>
public class LoafModelCustomizer : ModelCustomizer
{
    public IServiceProvider Provider { get; }
    public List<IModelCustomizing> Customizers { get; }

    public LoafModelCustomizer(ModelCustomizerDependencies dependencies ) : base(dependencies)
    {
        Customizers = typeof(LoafModelCustomizer).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IModelCustomizing)))
            .Select(type => Activator.CreateInstance(type) as IModelCustomizing).ToList();
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        base.Customize(modelBuilder, context);
        modelBuilder.ApplyConfigurationsFromAssembly(context.GetType().Assembly);
         
        foreach (var customizer in Customizers)
        {
            customizer.Customize(modelBuilder, context);
        }
    }
}
