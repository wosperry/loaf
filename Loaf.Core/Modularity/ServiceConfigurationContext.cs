using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Modularity;

public class ServiceConfigurationContext
{
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }

    public ServiceConfigurationContext(IServiceCollection services,IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }
}