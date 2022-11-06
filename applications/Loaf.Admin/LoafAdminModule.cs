using Loaf.Admin.EntityFramework;
using Loaf.Core.Encryptors;
using Loaf.Core.Modularity;
using Loaf.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loaf.Admin;

[DependsOn(
    typeof(Md5EncryptModule)
    )]
public class LoafAdminModule : LoafModule
{
    public override void ConfigureService(LoafModuleContext context)
    {
        context.Services
            .AddAutoMapper(typeof(LoafAdminModule).Assembly)
            .AddSwaggerDocument()
            .AddLoafDbContext<LoafAdminDbContext>((options, connectionString) =>
        {
            options.UseNpgsql(connectionString);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
        })
            .AddControllers();
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