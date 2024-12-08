using System;
using Shop.API.Mock.Models.Product;

namespace Shop.API.Mock.Data;

public class MockAppContext
{
    RandomData _randomData;
    public List<Product> Products;
    public MockAppContext()
    {
        _randomData = new RandomData();
        Products = [];

        foreach(var i in Enumerable.Range(1, 400))
        {
            var rating = _randomData.RandomObj.Next(1, 100);
            var name = string.Join("",$" ID:{i} ",$" R:{rating} ", _randomData.GetRandomName() );

            Product prod = new Product();

            prod.Id = i;
            prod.Name = name;
            prod.Units = "шт.";
            prod.Count = 0;
            prod.Price = _randomData.RandomObj.Next(500, 1000) / 100;
            prod.Discount = _randomData.RandomObj.Next(0, 100); 
            prod.Rating = rating;
            prod.ImageUrls = _randomData.GetRandomImageUrls();
            prod.PromoTag = "";
            prod.Category = _randomData.GetRandomCategory();
            prod.Description = _randomData.GetRandomProductDescription();
            prod.StockKeepingUnit = _randomData.GetRandomSKU();
            Products.Add(prod);
        }

    }
}