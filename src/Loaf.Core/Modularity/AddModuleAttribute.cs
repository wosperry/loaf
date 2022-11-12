using System;

namespace Loaf.Core.Modularity;

[AttributeUsage(AttributeTargets.Class)]
public class AddModuleAttribute : Attribute
{
    /// <summary>
    /// 依赖的模块
    /// </summary>
    public Type[] ModuleTypes { get; }

    public AddModuleAttribute(params Type[] moduleTypes)
    {
        ModuleTypes = moduleTypes;
    }
}