using Loaf.Core.Modularity;
using Loaf.EntityFrameworkCore;
using Loaf.EntityFrameworkCore.Tests.EFCore;
using Microsoft.EntityFrameworkCore;

public class MyModule : LoafModule
{
    public override void ConfigureService(LoafModuleContext context)
    {
        context.Services.AddControllers();
        context.Services.AddSwaggerDocument();
        context.Services.AddLoafDbContext<MyDbContext>((options,conn) =>
        {
            options.UseNpgsql(conn);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
        });
    }

    public override void Initialize(IApplicationBuilder app)
    { 
        app.UseAuthorization();
        app.UseOpenApi();
        app.UseSwaggerUi3();
        (app as WebApplication)!.MapControllers();
    }
}