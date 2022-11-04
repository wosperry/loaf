using Loaf.Admin;
using Loaf.Core.Modularity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLoafModule<LoafAdminModule>(builder.Configuration);

var app = builder.Build();

app.Initialize();

app.Run();