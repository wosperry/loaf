using System;

namespace Loaf.Core.Modularity;

[AttributeUsage(AttributeTargets.Class)]
public class DependsOnAttribute : Attribute
{
    /// <summary>
    /// 依赖的模块
    /// </summary>
    public Type[] ModuleTypes { get; }
    public DependsOnAttribute(params Type[] moduleTypes)
    {
        ModuleTypes = moduleTypes;
    }
}