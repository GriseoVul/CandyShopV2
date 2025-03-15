using System;
using System.Threading.Tasks;
using Shop.API.Entities;
using Shop.API.Entities.Category;
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
        const int RATING_MAX = 100;
        const int PRODUCT_COUNT = 400;
        
        foreach(var category in _categories)
        {
            var newCategory = new Category()
            {
                Id = index++,
                Name = category,
                //Characteristics = 
            };
            var newCharacterNames = Random.Shared.GetItems(_characteristics, Random.Shared.Next(3, 6));
            foreach (var name in newCharacterNames)
            {
                newCategory.Characteristics.Add(
                    new(
                        name, 
                        newCategory.Id, 
                        newCategory
                    )
                );
            }
            _context.Categories.Add(newCategory);
        }
        await _context.SaveChangesAsync();
        
        foreach(var i in Enumerable.Range(1, PRODUCT_COUNT))
        {
            var rating = _randomData.RandomObj.Next(1, RATING_MAX);
            var name = string.Join("",$" ID:{i} ",$" R:{rating} ", _randomData.GetRandomName() );
            var images = new List<ImageUrl>();
            var randomCategory = _context.Categories.Find( _randomData.RandomObj.Next(1, _categories.Length) );
            if (randomCategory is null) throw new InvalidOperationException("Category not found");

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
                Price = rating,
                Discount = _randomData.RandomObj.Next(0, 100),
                Rating =  _randomData.RandomObj.Next(1, RATING_MAX),
                ImageUrls = images,
                PromoTag = "",
                //category
                //Descriptiton
                SKU = _randomData.GetRandomSKU(),
                Category = randomCategory,
            };
            product.CategoryName = product.Category.Name;

            product.Description = new ProductDescription() {
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
            foreach (var cc in product.Category.Characteristics)
            {
                product.Description.Characterisitcs.Add(
                    new ProductCharacteristic()
                    {
                        Name = cc.Name,
                        Value = _randomData.RandomObj.Next(10, 1000).ToString()
                    }
                );
            }
            
            _context.Products.Add(product);
            
        }
        _context.SaveChanges();
    }
}
