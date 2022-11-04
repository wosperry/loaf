using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Modularity;

public class LoafModuleContext
{
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }

    public LoafModuleContext(IServiceCollection services,IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }
}