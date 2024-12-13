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
using Shop.API.Mock.Models.Category;
using Shop.API.Mock.Models.Product;
using Shop.API.Mock.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{Title = "Shop.Mock", Version="v1"});
    
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "https://localhost:7283",
        Description = "Dev tunnel local https"
    });
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "http://localhost:5267",
        Description = "Dev tunnel local https"
    });
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "https://gc2lch20-5267.euw.devtunnels.ms/",
        Description = "Dev tunnel http"
    });
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "https://gc2lch20-7283.euw.devtunnels.ms/",
        Description = "Dev tunnel https"
    });
}
);
// builder.Services.AddHealthChecks();

builder.Services.AddSingleton<MockAppContext>();


builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();

//builder.Services.AddOpenApi();

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
        // options =>
        //     options.SwaggerEndpoint("/openapi/v1.json", "v1")
    );
    app.Use(async (context, next) => 
    {
        var host = context.Request.Host.Value;
        if (!string.IsNullOrEmpty(host))
        {
            context.Response.Headers.Add("X-Forwarded-Host", host);
        }
        if(context.Request.Path.StartsWithSegments("/swagger"))
        {
            var serverUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
            var swaggerJson = new 
            {
                swagger = "2.0",
                info = new {title = "Candyshop API", version="v1"},
                host = context.Request.Host.Value,
                basePath = "/",
                schemes = new[] { context.Request.Scheme }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(swaggerJson);
            await context.Response.WriteAsync(json);
        }
        await next();
    });
}

using(var scope = app.Services.CreateScope() )
{
    var services = scope.ServiceProvider;

    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

//app.MapOpenApi().CacheOutput();

// app.UseHttpsRedirection();

app.UseRouting();
// app.UseHealthChecks("/health");
app.UseCors();

app.MapGet("/Products", 
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
    
    var totalPages = 5;//Math.Ceiling((float)ProdRequest.Count / request.PageLimit);

    return Results.Ok(new 
    {
        ItemsPerPage = request.PageLimit,
        TotalItems = ProdRequest.Count,
        CurrentPage = request.Page,
        TotalPages = totalPages,
        Items = ProdRequest
    });
})
.WithName("GetAllProducts")
.WithOpenApi();

app.MapGet("/Products/{ID:int}", 
[EndpointSummary("Get product by ID")]
[EndpointDescription("This endpoint return product object or NotFound code\n")]
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

app.MapGet("/Products/{SKU}", 
[EndpointSummary("Get product by SKU")]
[EndpointDescription("This endpoint return product object or NotFound code\n")]
[ProducesResponseType<Product>(200)]
[ProducesResponseType(404)]
( string SKU, IProductService service) =>
{
    var result = service.GetProductBySKU(SKU) ;
    if (result == null)
        return Results.NotFound( new { message = $"Product with SKU: {SKU} not found!"});
    return Results.Ok(result);
})
.WithName("GetProductBySKU")
.WithOpenApi();

app.MapGet("/Categories", 
    [EndpointSummary("Get list of the categories")]
    [EndpointDescription("This endpoint return object, that contains list of categories\n")]
    [ProducesResponseType<List<Product>>(200)]
    (
        ICategoryService service, 

        int? Page = 1, 
        int? PageLimit = 50
    )=>
{
    
    var Categories = service.GetAll();
    
    var totalPages = Math.Ceiling((float)Categories.Count / PageLimit??50);

    return Results.Ok(new 
    {
        ItemsPerPage = PageLimit??50,
        TotalItems = Categories.Count,
        CurrentPage = Page??1,
        TotalPages = totalPages,
        Items = Categories
    });
})
.WithName("GetAllCategories")
.WithOpenApi();

app.MapGet("/Categories/{ID:int}", 
[EndpointSummary("Get category by ID")]
[EndpointDescription("This endpoint return category object or NotFound code\n")]
[ProducesResponseType<Category>(200)]
[ProducesResponseType(404)]
( int ID, ICategoryService service) =>
{
    var result = service.GetCategoryById(ID) ;
    if (result == null)
        return Results.NotFound( new { message = $"Category with ID: {ID} not found!"});
    return Results.Ok(result);
})
.WithName("GetCategoryByID")
.WithOpenApi();

app.MapGet("/Categories/{Name}", 
[EndpointSummary("Get category by name")]
[EndpointDescription("This endpoint return category object or NotFound code\n")]
[ProducesResponseType<Category>(200)]
[ProducesResponseType(404)]
( string Name, ICategoryService service) =>
{
    var result = service.GetCategoryByName(Name) ;
    if (result == null)
        return Results.NotFound( new { message = $"Category with Name: {Name} not found!"});
    return Results.Ok(result);
})
.WithName("GetCategoryByName")
.WithOpenApi();
// app.MapGet("/HealthCheck", ( ) =>
// {
//     var result = GC.GetTotalMemory(true);
//     return Results.Ok(new {Bytes = result, Mb = (float)result/1_048_576 });
// })
// .WithName("GetTotalMemory")
// .WithOpenApi();

app.Run();

