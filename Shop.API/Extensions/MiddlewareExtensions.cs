using System;

namespace Shop.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this WebApplication app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop.API v1");
                // c.RoutePrefix = string.Empty;
            });
        }
        app.Use(
            async (context, next) =>
            {
                var host = context.Request.Host.Value;
                if (!string.IsNullOrEmpty(host))
                {
                    context.Response.Headers.Add("X-Forwarded-Host", host);
                }
                if (context.Request.Path.StartsWithSegments("/swagger"))
                {
                    var serverUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
                    var swaggerJson = new
                    {
                        swagger = "2.0",
                        info = new { title = "Candyshop API", version = "v1" },
                        host = context.Request.Host.Value,
                        basePath = "/",
                        schemes = new[] { context.Request.Scheme }
                    };
                    var json = System.Text.Json.JsonSerializer.Serialize(swaggerJson);
                    await context.Response.WriteAsync(json);
                }
                await next();
            }
        );
        app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigins");
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
