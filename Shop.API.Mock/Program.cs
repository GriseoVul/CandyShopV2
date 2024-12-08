using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Caching.Memory;
using Shop.API.Mock.Data;
using Shop.API.Mock.Models.Product;
using Shop.API.Mock.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddHealthChecks();

builder.Services.AddSingleton<RandomData>();
builder.Services.AddSingleton<MockAppContext>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

builder.Services.AddOutputCache( options =>
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(5)))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
            options.SwaggerEndpoint("/openapi/v1.json", "v1")
    );
}

app.MapOpenApi().CacheOutput();

app.UseHttpsRedirection();

// app.UseHealthChecks("/health");

app.MapGet("/Product", 
    [EndpointSummary("Get list of the products")]
    [EndpointDescription("This endpoint return object, that contains list of product\n")]
    [ProducesResponseType<List<Product>>(200)]
    (
        IProductService service, 
        string? Category, 
        string? Name, 
        [Description(@"
        Can take following values:
        0 - No sorting : default
        1 - PriceDescending
        2 - PriceAscending
        ")]
        SortOptions Sort = SortOptions.None, 
        int? Page = 1, 
        int? PageLimit = 50
    )=>
{
    
    var request = new ProductRequest()
        {
            Page = Page.GetValueOrDefault(),
            PageLimit = PageLimit.GetValueOrDefault(),
            Options = new ProductRequestOptions ()
            {
                Category = Category,
                Name = Name,
                Sort = Sort
            }
        };

    var ProdRequest = service.GetProductsWithParams(request);
    
    var totalPages = Math.Ceiling((float)ProdRequest.Count() / request.PageLimit);

    return Results.Ok(new 
    {
        ItemsPerPage = request.PageLimit,
        TotalItems = ProdRequest.Count(),
        CurrentPage = request.Page,
        TotalPages = totalPages,
        Items = ProdRequest
    });
})
.WithName("GetAllProducts")
.WithOpenApi();

app.MapGet("/Product/{ID}", 
[ProducesResponseType<Product>(200)]
[ProducesResponseType(404)]
( int ID, IProductService service) =>
{
    var result = service.GetProduct(ID) ;
    if (result == null)
        return Results.NotFound( new { message = $"Product with ID: {ID} not found!"});
    return Results.Ok(result);
})
.WithName("GetProductByID")
.WithOpenApi();

// app.MapGet("/HealthCheck", ( ) =>
// {
//     var result = GC.GetTotalMemory(true);
//     return Results.Ok(new {Bytes = result, Mb = (float)result/1_048_576 });
// })
// .WithName("GetTotalMemory")
// .WithOpenApi();

app.Run();

