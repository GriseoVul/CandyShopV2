using System;
using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Services.Implementations;
using Shop.API.Services.Interfaces;

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

        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseInMemoryDatabase("MockDatabase")
        );

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICategoryService, CategoryService>();
        return services;
    }
}
