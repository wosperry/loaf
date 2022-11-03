using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Modularity;

public class ServiceConfigurationContext
{
    public IServiceCollection Services { get; set; }

    public ServiceConfigurationContext(IServiceCollection services)
    {
        Services = services;
    }
}