using System;
using Shop.API.Mock.Models.Category;
using Shop.API.Mock.Models.Product;
using Shop.API.Mock.Services;

namespace Shop.API.Mock.Data;

public static class SeedData
{
    
    public static void Initialize(IServiceProvider serviceProvider)
    {
        IProductService _productService = serviceProvider.GetRequiredService<IProductService>();
        ICategoryService _categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        RandomData _randomData = new();
        MockAppContext _context = serviceProvider.GetRequiredService<MockAppContext>();


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
            _context.Categories.Add(new Category()
            {
                Id = index++,
                Name = category,
                Characteristics = Random.Shared.GetItems(_characteristics, Random.Shared.Next(3, 6))
            });
        }

        foreach(var i in Enumerable.Range(1, 400))
        {
            var rating = _randomData.RandomObj.Next(1, 100);
            var name = string.Join("",$" ID:{i} ",$" R:{rating} ", _randomData.GetRandomName() );

            Product product = new()
            {
                Id = i,
                Name = name,
                Units = "шт.",
                Count = 0,
                Price = _randomData.RandomObj.Next(500, 1000) / 100,
                Discount = _randomData.RandomObj.Next(0, 100),
                Rating = rating,
                ImageUrls = _randomData.GetRandomImageUrls(),
                PromoTag = "",
                //category
                //Descriptiton
                SKU = _randomData.GetRandomSKU()
            };
            var randomCategory = _categoryService.GetCategoryById(_randomData.RandomObj.Next(1, _context.Categories.Count - 1));

            product.Category = randomCategory?.Name ?? "$#&a&*%na781";

            product.Description = new ProductDescription() {
                Characterisitcs = (from c in randomCategory?.Characteristics 
                                   select new ProductCharacteristic{
                                    Name = c, 
                                    Value = _randomData.RandomObj.Next(10, 1000).ToString() 
                                    }).ToArray(),
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
            _productService.AddProduct(product);

        }
    }
}
