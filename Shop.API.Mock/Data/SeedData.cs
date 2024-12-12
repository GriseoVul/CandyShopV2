using System;
using Shop.API.Mock.Models.Category;
using Shop.API.Mock.Models.Product;
using Shop.API.Mock.Services;

namespace Shop.API.Mock.Data;

public class SeedData
{
    private readonly MockAppContext _context;
    private readonly RandomData _randomData;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    private void FillProducts()
    {
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
                StockKeepingUnit = _randomData.GetRandomSKU()
            };
            var randomCategory = _categoryService.GetCategoryById(_randomData.RandomObj.Next(1, _context.Categories.Count - 1));
            product.Category = randomCategory.Name ?? "$#&a&*%na781";
            product.Description = 
            _context.Products.Add(product);
        }
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
            _context.Categories.Add(new Category()
            {
                Id = index++,
                Name = category,
                Characteristics = Random.Shared.GetItems(_characteristics, Random.Shared.Next(3, 6))
            });
        }
    }
    
    public SeedData
    ( 
        MockAppContext context, 
        IProductService productService, 
        ICategoryService categoryService 
    )
    {
        _context = context;
        _randomData = new();
        _productService = productService;
        _categoryService = categoryService;

        FillCategories();
        FillProducts();
    }
}
