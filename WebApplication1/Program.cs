using Loaf.Core.Modularity;

var builder = WebApplication.CreateBuilder(args); 

var app = builder.Build();

app.UseOpenApi().UseSwaggerUi3();

app.UseAuthorization();

app.MapControllers();

app.Run();

 