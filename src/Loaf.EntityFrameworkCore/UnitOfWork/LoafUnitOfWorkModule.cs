using Loaf.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.EntityFrameworkCore.UnitOfWork
{
    public class LoafUnitOfWorkModule : LoafModule
    {
        public override void ConfigureService(LoafModuleContext context)
        {
            context.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public override void PostInitialize(IApplicationBuilder app)
        {
            app.UseMiddleware<LoafUnitOfWokMiddleware>();
        }
    }
}