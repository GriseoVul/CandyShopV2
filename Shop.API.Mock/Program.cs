using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
var categories = new[]
{
    "шоколад", "мармелад", "карамель", "конфеты", "халва", "драже", "печенье", "пряники", "торты", "кексы", "вафли", "пирожные"
};
var images = new [] {"https://timeweb.com/media/articles/0001/18/thumb_17628_articles_standart.png",
            "https://timeweb.com/media/articles/0001/18/thumb_17629_articles_standart.png",
            "https://timeweb.com/media/articles/0001/18/thumb_17638_articles_standart.png",
            "https://i.pinimg.com/736x/a7/da/f2/a7daf24393f0ba0d8282d9e720e88510.jpg",
            "https://wallpaper.forfun.com/fetch/74/74d2c5f8c46325771cab9ac7613fb04f.jpeg?h=900&r=0.5",
            "https://cdn1.ozone.ru/s3/multimedia-o/6063449232.jpg",
            "https://timeweb.com/media/articles/0001/18/thumb_17634_articles_standart.png",
            "https://timeweb.com/media/articles/0001/18/thumb_17631_articles_standart.png"
            };

var RandomObj = new Random();
var Products = Enumerable.Range(1, 400).Select(index =>
        new Product
        (
            index,
            $"TestProduct{index}",
            "шт.",
            0,
            RandomObj.Next(500, 1000) / 100,
            RandomObj.Next(0, 100), 
            RandomObj.Next(1, 100),
            RandomObj.GetItems(images, 3),
            "",
            RandomObj.GetItems(categories, 1)[0],
            new ProductDescription() {
                Characterisitcs = Enumerable.Range(1, 5).Select(index => 
                    new ProductCharacteristic ()
                    {
                        Name = $"Characterisitc{index}",
                        Value = $"Value{index}"
                    }
                ).ToArray(),
                About = "Вот вам яркий пример современных тенденций — новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующих условий активизации. Учитывая ключевые сценарии поведения, экономическая повестка сегодняшнего дня предполагает независимые способы реализации переосмысления внешнеэкономических политик. Таким образом, существующая теория напрямую зависит от экономической целесообразности принимаемых решений. Внезапно, интерактивные прототипы освещают чрезвычайно интересные особенности картины в целом, однако конкретные выводы, разумеется, призваны к ответу. Современные технологии достигли такого уровня, что начало повседневной работы по формированию позиции не даёт нам иного выбора, кроме определения первоочередных требований. Но семантический разбор внешних противодействий позволяет выполнить важные задания по разработке переосмысления внешнеэкономических политик.",
                FoodValue = RandomObj.Next(0, 100) > 50 ? null : new FoodValue 
                    (
                        RandomObj.Next(100),
                        RandomObj.Next(100),
                        RandomObj.Next(100),
                        RandomObj.Next(100)
                    ){}
            }           
        )
    ).OrderByDescending(Pr => Pr.Rating);




app.MapGet("/Product", ( string? Category, string? Name, SortOptions? Sort, int? Page = 1, int? PageLimit = 50) =>
{
    var request = new ProductRequest()
        {
            Page = Page,
            PageLimit = PageLimit,
            Options = new ProductRequestOptions ()
            {
                Category = Category,
                Name = Name,
                Sort = Sort
            }
        };

    var ProdRequest = Products.TakeLast((request.Page ?? 1 - 1) * request.PageLimit??50 + request.PageLimit??50 );
    
    switch (request.Options.Sort)
    {
        case SortOptions.PriceAscending:
            ProdRequest = from prod in ProdRequest orderby prod.Rating descending,prod.Price select prod;
        break;

        case SortOptions.PriceDescending:
            ProdRequest = from prod in ProdRequest orderby prod.Rating descending, prod.Price descending select prod;
        break;
    }

    return Results.Ok(new 
    {
        TotalItems = ProdRequest.Count(),
        CurrentPage = request.Page,
        TotalPages = 10,
        Items = ProdRequest.ToArray()
    });
})
.WithName("GetAllProducts")
.WithOpenApi();

app.MapGet("/Product/{ID}", ( int ID) =>
{
    var result = Products.Where(pr => pr.Id == ID);
    if (result.LastOrDefault() == null)
        return Results.NotFound( new { message = $"ID: {ID} not found!"});
    return Results.Ok(result.LastOrDefault());
})
.WithName("GetProductByID")
.WithOpenApi();

app.MapGet("/HealthCheck", ( ) =>
{
    var result = GC.GetTotalMemory(true);
    return Results.Ok(new {Bytes = result, Mb = (float)result/1_048_576 });
})
.WithName("GetTotalMemory")
.WithOpenApi();

app.Run();

public enum SortOptions
{
    None,
    PriceDescending,
    PriceAscending
}

public class ProductRequestOptions()
{
    public string? Name { get; set; } = "";
    public string? Category { get; set; } = "";
    public SortOptions? Sort { get; set; } = SortOptions.None;
}
public class ProductRequest
{
    public int? Page { get; set; }
    public int? PageLimit { get; set; }
    public ProductRequestOptions? Options {get; set;}

}
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record Product(
    int Id,
    String Name, 
    String Units,
    int Count,
    float Price, 
    int Discount,
    int Rating,
    string[] ImageUrls,
    string PromoTag,
    string Category,
    ProductDescription Description
    )
{
    public float TotalPrice => Price - (Price / 100 * Discount);
    public bool InStock => Count > 0;
}
record ProductDescription()
{
    public ProductCharacteristic[] Characterisitcs {get; set;} = [];
    public string About { get; set; } = "";
    public FoodValue? FoodValue { get; set;} = null;
}

record ProductCharacteristic()
{
    public string Name {get; set; } = "";
    public string Value {get; set; } = "";
}

record FoodValue (
    float Protein,
    float Fat,
    float Carbohydrate,
    float Calories
){}