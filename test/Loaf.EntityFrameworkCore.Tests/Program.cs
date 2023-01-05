using Loaf.Core.Modularity;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLoafModule<MyModule>();
var app = builder.Build();
app.Initialize();
app.Run();
