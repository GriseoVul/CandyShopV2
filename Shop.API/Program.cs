using Shop.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

app.UseCustomMiddleware(app.Environment);

app.Run();
