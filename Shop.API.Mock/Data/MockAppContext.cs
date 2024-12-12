using System;
using Shop.API.Mock.Models.Category;
using Shop.API.Mock.Models.Product;

namespace Shop.API.Mock.Data;

public class MockAppContext
{
    RandomData _randomData;
    public List<Product> Products;
    public List<Category> Categories;

    public MockAppContext()
    {
        _randomData = new RandomData();
        Products = [];
        Categories = [];

        FillCategories();
        FillProducts();
    }
    private void FillCategories()
    {
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
            Categories.Add(new Category()
            {
                Id = index++,
                Name = category,
                Characteristics = Random.Shared.GetItems(_characteristics, Random.Shared.Next(3, 6))
            });
        }
    }
    
    private void FillProducts()
    {
        foreach(var i in Enumerable.Range(1, 400))
        {
            var rating = _randomData.RandomObj.Next(1, 100);
            var name = string.Join("",$" ID:{i} ",$" R:{rating} ", _randomData.GetRandomName() );

            Product prod = new()
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
                Category = _randomData.GetRandomCategory(),
                Description = _randomData.GetRandomProductDescription(),
                StockKeepingUnit = _randomData.GetRandomSKU()
            };
            Products.Add(prod);
        }
    }
}