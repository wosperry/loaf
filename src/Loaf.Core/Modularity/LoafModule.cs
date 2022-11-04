using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Loaf.Core.Modularity;

public abstract class LoafModule
{
    public virtual void PreConfigureService(LoafModuleContext context)
    {
    }

    public virtual void ConfigureService(LoafModuleContext context)
    {
    }

    public virtual void PostConfigureService(LoafModuleContext context)
    {
    }

    public virtual void PreInitialize(IApplicationBuilder app)
    {
        
    }
    public virtual void Initialize(IApplicationBuilder app)
    {
        
    }
    public virtual void PostInitialize(IApplicationBuilder app)
    {
        
    }
}