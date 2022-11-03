namespace Loaf.Core.Modularity;

public abstract class LoafModule
{
    public virtual void PreConfigureService(ServiceConfigurationContext context)
    {
    }

    public virtual void ConfigureService(ServiceConfigurationContext context)
    {
    }

    public virtual void PostConfigureService(ServiceConfigurationContext context)
    {
    }
}