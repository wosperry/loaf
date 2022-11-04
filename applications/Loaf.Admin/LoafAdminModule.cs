using Loaf.Admin.EntityFramework;
using Loaf.Core.Modularity;
using Loaf.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loaf.Admin;

public class LoafAdminModule : LoafModule
{
    public override void ConfigureService(LoafModuleContext context)
    {
        context.Services.AddControllers();
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerDocument();
        context.Services.AddLoafDbContext<LoafAdminDbContext>((options, connectionString) =>
        {
            options.UseSqlite(connectionString);
        });
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