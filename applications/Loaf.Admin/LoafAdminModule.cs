using Loaf.Core.Modularity; 

namespace Loaf.Admin;

public class LoafAdminModule : LoafModule
{
    public override void ConfigureService(LoafModuleContext context)
    {
        context.Services.AddControllers();
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerDocument();
    }

    public override void Initialize(IApplicationBuilder application)
    {
        var app = (application as WebApplication)!;

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}