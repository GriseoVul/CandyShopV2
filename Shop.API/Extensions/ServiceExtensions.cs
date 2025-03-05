using System;

namespace Shop.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddMemoryCache();
        services.AddOutputCache( options =>
            options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(5)))
        );

        services.AddCors(options => 
            options.AddDefaultPolicy(builder => 
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod()
            )
        );

        return services;
    }
}
