using System;
using System.Threading.Tasks;
using Shop.API.Entities;
using Shop.API.Entities.Product;
using Shop.API.Services.Interfaces;

namespace Shop.API.Data;

 public static class SeedData
{

    public static async Task Initialize(ApplicationDBContext _context)
    {
        RandomData _randomData = new();
        
        string[] _categories = [
            "шоколад", "мармелад", "карамель", 
            "конфеты", "халва", "драже", 
            "печенье", "пряники", "торты", 
            "кексы", "вафли", "пирожные"
        ];
        string[] _characteristics = [
            "Мощность", "Вес", "Объём", 
            "Длина", "Ширина", "Высота", 
            "Скорость", "Частота", "Температура", 
            "Время работы", "Время зарядки", "Максимальная нагрузка", 
            "Энергопотребление", "Класс энергоэффективности", "Уровень шума", 
            "Рейтинг", "Количество функций", "Глубина", 
            "Диаметр", "Площадь"
        ];

        int index = 1;
        foreach(var category in _categories)
        {
            _context.Categories.Add(new Entities.Category.Category()
            {
                Id = index++,
                Name = category,
                //Characteristics = Random.Shared.GetItems(_characteristics, Random.Shared.Next(3, 6))
            });
            
        }

        
        foreach(var i in Enumerable.Range(1, 400))
        {
            var rating = _randomData.RandomObj.Next(1, 100);
            var name = string.Join("",$" ID:{i} ",$" R:{rating} ", _randomData.GetRandomName() );
            var images = new List<ImageUrl>();
            foreach (var url in _randomData.GetRandomImageUrls())
            {
                images.Add(new(url));
            }
            Product product = new(name)
            {
                Id = i,
                //name
                Units = "шт.",
                Count = 0,
                Price = _randomData.RandomObj.Next(500, 1000) / 100,
                Discount = _randomData.RandomObj.Next(0, 100),
                Rating = rating,
                ImageUrls =  images,
                PromoTag = "",
                //category
                //Descriptiton
                SKU = _randomData.GetRandomSKU()
            };
            var randomCategory = _context.Categories.Where( c => c.Id == _randomData.RandomObj.Next(1, _context.Categories.Count() - 1) ).FirstOrDefault();

            product.Category = randomCategory ?? null!;
            product.CategoryName = randomCategory?.Name ?? "$#&a&*%na781";

            product.Description = new ProductDescription() {
                Characterisitcs = [.. from c in randomCategory?.Characteristics 
                                    select new ProductCharacteristic{
                                    Name = c.Name, 
                                    Value = _randomData.RandomObj.Next(10, 1000).ToString() 
                                    }],
                About = _randomData.GetRandomAbout(),
                FoodValue = _randomData.RandomObj.Next(0, 100) > 50 ? 
                null : 
                new FoodValue 
                    {
                        Protein = _randomData.RandomObj.Next(100),
                        Fat = _randomData.RandomObj.Next(100),
                        Carbohydrate = _randomData.RandomObj.Next(100),
                        Calories = _randomData.RandomObj.Next(100)
                    }
            };
            
            //await _productService.CreateAsync(product);
            _context.Products.Add(product);
        }
        _context.SaveChanges();
    }
}
