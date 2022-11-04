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
}