using Shop.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// builder.Logging.AddDebug();


var app = builder.Build();

await app.UseCustomMiddlewareAsync(app.Environment);

app.Run();
