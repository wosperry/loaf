using Loaf.Core.Modularity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddModule<LoafAdminModule>(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class LoafAdminModule: LoafModule
{
    public override void ConfigureService(LoafModuleContext context)
    {
        context.Services.AddControllers();
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerDocument();
    }
}