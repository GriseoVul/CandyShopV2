using Shop.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();

var app = builder.Build();

app.UseCustomMiddleware(app.Environment);

app.Run();
